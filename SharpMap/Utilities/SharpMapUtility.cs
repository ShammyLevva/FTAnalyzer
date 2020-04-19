using NetTopologySuite;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace SharpMap
{
    public static class SharpMapUtility
    {
        /// <summary>
        /// Method to configure the SharpMap session.
        /// </summary>
        public static void Configure()
        {
            var gss = new NtsGeometryServices();
            var css = new SharpMap.CoordinateSystems.CoordinateSystemServices(
            new CoordinateSystemFactory(),
            new CoordinateTransformationFactory(),
            Converters.WellKnownText.SpatialReference.GetAllReferenceSystems());

            // plug-in WebMercator so that correct spherical definition is directly available to Layer Transformations using SRID
            var pcs = ProjectedCoordinateSystem.WebMercator;
            css.AddCoordinateSystem((int)pcs.AuthorityCode, pcs);
            Session.Instance
                .SetGeometryServices(gss)
                .SetCoordinateSystemServices(css)
                .SetCoordinateSystemRepository(css);
        }
    }
}
