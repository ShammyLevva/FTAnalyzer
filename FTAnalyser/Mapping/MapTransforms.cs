using System.Collections.Generic;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace FTAnalyzer.Mapping
{
    class MapTransforms
    {
        private static IProjectedCoordinateSystem GetEPSG900913(CoordinateSystemFactory csFact)
        {
            List<ProjectionParameter> parameters = new List<ProjectionParameter>
            {
                new ProjectionParameter("semi_major", 6378137.0),
                new ProjectionParameter("semi_minor", 6378137.0),
                new ProjectionParameter("latitude_of_origin", 0.0),
                new ProjectionParameter("central_meridian", 0.0),
                new ProjectionParameter("scale_factor", 1.0),
                new ProjectionParameter("false_easting", 0.0),
                new ProjectionParameter("false_northing", 0.0)
            };
            IProjection projection = csFact.CreateProjection("Google Mercator", "mercator_1sp", parameters);
            IGeographicCoordinateSystem wgs84 = csFact.CreateGeographicCoordinateSystem(
                "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East)
            );

            IProjectedCoordinateSystem epsg900913 = csFact.CreateProjectedCoordinateSystem("Google Mercator", wgs84, projection, LinearUnit.Metre,
              new AxisInfo("East", AxisOrientationEnum.East), new AxisInfo("North", AxisOrientationEnum.North));
            return epsg900913;
        }

        /// <summary>
        /// Wgs84 to Google Mercator Coordinate Transformation
        /// </summary>
        public static ICoordinateTransformation Wgs84toGoogleMercator
        {
            get
            {
                if (wgs84toGoogle == null)
                {
                    CoordinateSystemFactory csFac = new CoordinateSystemFactory();
                    CoordinateTransformationFactory ctFac = new CoordinateTransformationFactory();

                    IGeographicCoordinateSystem wgs84 = csFac.CreateGeographicCoordinateSystem(
                      "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                      new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East));

                    List<ProjectionParameter> parameters = new List<ProjectionParameter>
                    {
                        new ProjectionParameter("semi_major", 6378137.0),
                        new ProjectionParameter("semi_minor", 6378137.0),
                        new ProjectionParameter("latitude_of_origin", 0.0),
                        new ProjectionParameter("central_meridian", 0.0),
                        new ProjectionParameter("scale_factor", 1.0),
                        new ProjectionParameter("false_easting", 0.0),
                        new ProjectionParameter("false_northing", 0.0)
                    };
                    IProjection projection = csFac.CreateProjection("Google Mercator", "mercator_1sp", parameters);

                    IProjectedCoordinateSystem epsg900913 = csFac.CreateProjectedCoordinateSystem(
                      "Google Mercator", wgs84, projection, LinearUnit.Metre, new AxisInfo("East", AxisOrientationEnum.East),
                      new AxisInfo("North", AxisOrientationEnum.North));

                    ((CoordinateSystem)epsg900913).DefaultEnvelope = new[] { -20037508.342789, -20037508.342789, 20037508.342789, 20037508.342789 };

                    wgs84toGoogle = ctFac.CreateFromCoordinateSystems(wgs84, epsg900913);

                }

                return wgs84toGoogle;
            }
        }

        private static ICoordinateTransformation wgs84toGoogle;

        private static CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();
        private static CoordinateSystemFactory csFact = new CoordinateSystemFactory();

        private static ICoordinateTransformation Transform()
        {
            return ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, GetEPSG900913(csFact));
        }

        private static ICoordinateTransformation ReverseTransform()
        {
            return ctFact.CreateFromCoordinateSystems(GetEPSG900913(csFact), GeographicCoordinateSystem.WGS84); ;
        }

        //public static IMathTransform MathTransform
        //{
        //    get { return Transform().MathTransform; }
        //}

        public static GeoResponse.CResult.CGeometry.CViewPort TransformViewport(GeoResponse.CResult.CGeometry.CViewPort viewport)
        {
            Coordinate mNorthEast = TransformCoordinate(new Coordinate(viewport.NorthEast.Long, viewport.NorthEast.Lat));
            Coordinate mSouthWest = TransformCoordinate(new Coordinate(viewport.SouthWest.Long, viewport.SouthWest.Lat));
            GeoResponse.CResult.CGeometry.CViewPort result = new GeoResponse.CResult.CGeometry.CViewPort();
            result.NorthEast.Long = mNorthEast.X;
            result.NorthEast.Lat = mNorthEast.Y;
            result.SouthWest.Long = mSouthWest.X;
            result.SouthWest.Lat = mSouthWest.Y;
            return result;                        
        }

        public static GeoResponse.CResult.CGeometry.CViewPort ReverseTransformViewport(GeoResponse.CResult.CGeometry.CViewPort viewport)
        {
            Coordinate mNorthEast = ReverseTransformCoordinate(new Coordinate(viewport.NorthEast.Long, viewport.NorthEast.Lat));
            Coordinate mSouthWest = ReverseTransformCoordinate(new Coordinate(viewport.SouthWest.Long, viewport.SouthWest.Lat));
            GeoResponse.CResult.CGeometry.CViewPort result = new GeoResponse.CResult.CGeometry.CViewPort();
            result.NorthEast.Long = mNorthEast.X;
            result.NorthEast.Lat = mNorthEast.Y;
            result.SouthWest.Long = mSouthWest.X;
            result.SouthWest.Lat = mSouthWest.Y;
            return result;
        }

        public static Coordinate TransformCoordinate(Coordinate point)
        {
            return Transform().MathTransform.Transform(point);
        }

        public static Coordinate ReverseTransformCoordinate(Coordinate point)
        {
            return ReverseTransform().MathTransform.Transform(point);
        }
    }
}
