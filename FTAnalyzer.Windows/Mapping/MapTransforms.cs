using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace FTAnalyzer.Mapping
{
    class MapTransforms
    {
        private static ProjectedCoordinateSystem GetEPSG900913(CoordinateSystemFactory csFact)
        {
            List<ProjectionParameter> parameters =
            [
                new ProjectionParameter("semi_major", 6378137.0),
                new ProjectionParameter("semi_minor", 6378137.0),
                new ProjectionParameter("latitude_of_origin", 0.0),
                new ProjectionParameter("central_meridian", 0.0),
                new ProjectionParameter("scale_factor", 1.0),
                new ProjectionParameter("false_easting", 0.0),
                new ProjectionParameter("false_northing", 0.0)
            ];
            IProjection projection = csFact.CreateProjection("Google Mercator", "mercator_1sp", parameters);
            GeographicCoordinateSystem wgs84 = csFact.CreateGeographicCoordinateSystem(
                "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East)
            );

            ProjectedCoordinateSystem epsg900913 = csFact.CreateProjectedCoordinateSystem("Google Mercator", wgs84, projection, LinearUnit.Metre,
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
                if (wgs84toGoogle is null)
                {
                    CoordinateSystemFactory csFac = new();
                    CoordinateTransformationFactory ctFac = new();

                    GeographicCoordinateSystem wgs84 = csFac.CreateGeographicCoordinateSystem(
                      "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                      new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East));

                    List<ProjectionParameter> parameters =
                    [
                        new ProjectionParameter("semi_major", 6378137.0),
                        new ProjectionParameter("semi_minor", 6378137.0),
                        new ProjectionParameter("latitude_of_origin", 0.0),
                        new ProjectionParameter("central_meridian", 0.0),
                        new ProjectionParameter("scale_factor", 1.0),
                        new ProjectionParameter("false_easting", 0.0),
                        new ProjectionParameter("false_northing", 0.0)
                    ];
                    IProjection projection = csFac.CreateProjection("Google Mercator", "mercator_1sp", parameters);

                    ProjectedCoordinateSystem epsg900913 = csFac.CreateProjectedCoordinateSystem(
                      "Google Mercator", wgs84, projection, LinearUnit.Metre, new AxisInfo("East", AxisOrientationEnum.East),
                      new AxisInfo("North", AxisOrientationEnum.North));

                    epsg900913.DefaultEnvelope = [-20037508.342789, -20037508.342789, 20037508.342789, 20037508.342789];

                    wgs84toGoogle = ctFac.CreateFromCoordinateSystems(wgs84, epsg900913);

                }

                return wgs84toGoogle;
            }
        }

        static ICoordinateTransformation wgs84toGoogle;

        static readonly CoordinateTransformationFactory ctFact = new();
        static readonly CoordinateSystemFactory csFact = new();

        static ICoordinateTransformation Transform() => ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, GetEPSG900913(csFact));

        static ICoordinateTransformation ReverseTransform()
        {
            return ctFact.CreateFromCoordinateSystems(GetEPSG900913(csFact), GeographicCoordinateSystem.WGS84);
        }

        //public static MathTransform MathTransform
        //{
        //    get { return Transform().MathTransform; }
        //}

        public static GeoResponse.CResult.CGeometry.CViewPort TransformViewport(GeoResponse.CResult.CGeometry.CViewPort viewport)
        {
            Coordinate mNorthEast = TransformCoordinate(new Coordinate(viewport.NorthEast.Long, viewport.NorthEast.Lat));
            Coordinate mSouthWest = TransformCoordinate(new Coordinate(viewport.SouthWest.Long, viewport.SouthWest.Lat));
            GeoResponse.CResult.CGeometry.CViewPort result = new();
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
            GeoResponse.CResult.CGeometry.CViewPort result = new();
            result.NorthEast.Long = mNorthEast.X;
            result.NorthEast.Lat = mNorthEast.Y;
            result.SouthWest.Long = mSouthWest.X;
            result.SouthWest.Lat = mSouthWest.Y;
            return result;
        }

        public static Coordinate TransformCoordinate(Coordinate point)
        {
            var (x, y) = Transform().MathTransform.Transform(point.X, point.Y);
            return new Coordinate(x, y);
        }

        public static Coordinate ReverseTransformCoordinate(Coordinate point)
        {
            var (x, y) = ReverseTransform().MathTransform.Transform(point.X, point.Y);
            return new Coordinate(x, y);
        }
    }
}
