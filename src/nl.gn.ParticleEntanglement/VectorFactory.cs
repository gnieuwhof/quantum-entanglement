namespace nl.gn.ParticleEntanglement
{
    using System;
    using System.Windows.Media.Media3D;

    public static class VectorFactory
    {
        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static Vector3D Create(float angle)
        {
            double radians = angle * (Math.PI / 180.0);

            double x = Math.Cos(radians);
            double y = Math.Sin(radians);
            double z = 0;

            return new Vector3D(x, y, z);
        }

        public static Vector3D Create()
        {
            double x = (random.NextDouble() * 2 - 1);
            double y = (random.NextDouble() * 2 - 1);
            double z = (random.NextDouble() * 2 - 1);

            return new Vector3D(x, y, z);
        }
    }
}
