using NetTopologySuite;

namespace SharpMap
{
    /// <summary>
    /// A SharpMap session
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// The geometry services instance
        /// </summary>
        NtsGeometryServices GeometryServices { get; }

        /// <summary>
        /// Gets the coordinate system services instance
        /// </summary>
        CoordinateSystems.CoordinateSystemServices CoordinateSystemServices { get; }

        /// <summary>
        /// Gets the coordinate system repository
        /// </summary>
        ICoordinateSystemRepository CoordinateSystemRepository { get; }

#region Fluent configuration
        ISession SetGeometryServices(NtsGeometryServices geometryServices);
        ISession SetCoordinateSystemServices(CoordinateSystems.CoordinateSystemServices geometryServices);
        ISession SetCoordinateSystemRepository(ICoordinateSystemRepository geometryServices);
#endregion

    }
}