// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UtilityService.cs" company="CNC Software, Inc.">
//   Copyright (c) 2013 CNC Software, Inc.
// </copyright>
// <summary>
//   Defines the UtilityService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChainManagerExample.Services
{
    using System.Collections;
    using System.Linq;

    using Mastercam.Curves;
    using Mastercam.Database;
    using Mastercam.Database.Types;
    using Mastercam.GeometryUtility;
    using Mastercam.GeometryUtility.Types;
    using Mastercam.IO;
    using Mastercam.IO.Types;
    using Mastercam.Math;
    using Mastercam.Operations;
    using Mastercam.Support;
    using Mastercam.Tools;

    /// <summary>
    /// The utility service.
    /// </summary>
    public static class UtilityService
    {
        #region Internal Methods

        /// <summary>
        /// The initialize.
        /// </summary>
        internal static void Initialize()
        {
            FileManager.New(false);

            MachineDefManager.CreateMillMachineGroup();
        }

        /// <summary> Draws a block on the specified level. </summary>
        ///
        /// <param name="topPoint1">    The first top point. </param>
        /// <param name="topPoint2">    The second top point. </param>
        /// <param name="depth"> The depth. </param>
        /// <param name="level">        The level. </param>
        internal static void DrawBlock(Point3D topPoint1, Point3D topPoint2, double depth, int level)
        {
            SelectionManager.UnselectAllGeometry();

            // Create the top rectangle
            var topRectangle = GeometryCreationManager.CreateRectangle(topPoint1, topPoint2);

            foreach (var element in topRectangle)
            {
                element.Color = 48; // Green
                element.Level = level;
                element.Commit();
            }

            SelectionManager.SelectGeometryByMask(QuickMaskType.Lines);

            // Translate a copy down in Z for the bottom rectangle
            GeometryManipulationManager.TranslateGeometry(
                new Point3D(0, 0, topPoint1.z),
                new Point3D(0, 0, depth),
                ViewManager.GraphicsView,
                ViewManager.GraphicsView,
                true);

            SelectionManager.UnselectAllGeometry();

            // Create the vertices
            var verticalLines = new ArrayList
                                    {
                                        new LineGeometry(new Point3D(topPoint1.x, topPoint1.y, topPoint1.z), new Point3D(topPoint1.x, topPoint1.y, depth)),
                                        new LineGeometry(new Point3D(topPoint1.x, topPoint2.y, topPoint1.z), new Point3D(topPoint1.x, topPoint2.y, depth)),
                                        new LineGeometry(new Point3D(topPoint2.x, topPoint2.y, topPoint1.z), new Point3D(topPoint2.x, topPoint2.y, depth)),
                                        new LineGeometry(new Point3D(topPoint2.x, topPoint1.y, topPoint1.z), new Point3D(topPoint2.x, topPoint1.y, depth))
                                    };

            foreach (Geometry line in verticalLines)
            {
                line.Color = 48; // Green
                line.Level = level;
                line.Commit();
            }

            const int ViewNumber = (int)GraphicsViewType.Iso;

            ViewManager.GraphicsView = SearchManager.GetViews(ViewNumber)[0];
            GraphicsManager.FitScreen();
            GraphicsManager.ClearColors(new GroupSelectionMask());
        }

        /// <summary>
        /// The draw polygon.
        /// </summary>
        internal static void DrawPolygon()
        {
            var polyParams = new PolygonCreationParams
            {
                CenterPoint = new Point3D(0, 0, 0),
                FilletRadius = 0.2,
                NumberSides = 5,
                MeasureCorner = false,
                Radius = 5,
                RotationAngle = 45,
                ShowCenter = false,
                TrimmedSurface = false
            };

            // Draw a rectangle
            GeometryCreationManager.CreatePolygon(polyParams);
        }

        /// <summary> Draw rectangle. </summary>
        ///
        /// <returns> An array of Geometry. </returns>
        internal static Geometry[] DrawRectangle()
        {
            var lines = GeometryCreationManager.CreateRectangle(new Point3D(0, 0, 0), new Point3D(10, 10, 0));
            return lines.Cast<Geometry>().ToArray();
        }

        /// <summary> Shows the message. </summary>
        ///
        /// <param name="msg"> The message. </param>
        internal static void ShowMessage(string msg)
        {
            DialogManager.OK(msg, "Chain All");
        }

        /// <summary> Creates the tool. </summary>
        ///
        /// <returns> The new tool. </returns>
        internal static EndMillFlatTool CreateTool()
        {
            // Define a new tool
            var endMill = new EndMillFlatTool(.375, 0, 3, 3, 3, 4, 2, 3, "0.375 Flat End Mill")
            {
                Coolant = CoolantMode.COOL_FLOOD,
                Flutes = 2,
                MfgToolCode = "My Custom Code 1234",
                PlungeFeed = 12,
                RetractFeed = 12
            };

            // Commit the tool to the database
            return !endMill.Commit() ? null : endMill;
        }

        /// <summary> Creates contour operation. </summary>
        ///
        /// <param name="tool">   The tool. </param>
        /// <param name="chains"> The chains. </param>
        ///
        /// <returns> The new contour operation. </returns>
        internal static ContourOperation CreateContourOperation(EndMillFlatTool tool, Chain[] chains)
        {
            // Throw a contour around the chains
            var contourOp = new ContourOperation
            {
                OperationTool = tool,

                CutterComp =
                {
                    Direction = CutterCompDir.CutterCompLeft,
                    RollCorners = CutterCompRoll.CutterCompRollAll
                },

                Linking =
                {
                    Depth = -.25,
                    Retract = 1,
                    RetractOn = true,
                    TopStock = 0
                },

                SpindleSpeed = 2500,
                FeedRate = 12,
                NCIName = "Contour #1",
                Name = "Contour operation created via API"
            };

            ChainManager.StartChainAtLongest(chains);
            ChainManager.SortChains(chains, ChainManager.SortType.Area, ChainManager.SortOrder.Ascending);

            contourOp.SetChainArray(chains);
            if (!contourOp.Commit())
            {
                return null;
            }

            contourOp.Regenerate();

            return contourOp;
        }

        /// <summary>
        /// The fit screen.
        /// </summary>
        internal static void FitScreen()
        {
            // Set the Graphic view to ISO
            var views = SearchManager.GetViews((short)GraphicsViewType.Iso);
            ViewManager.GraphicsView = views[0];
            GraphicsManager.FitScreen();
        }

        #endregion
    }
}
