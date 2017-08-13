namespace nl.gn.ParticleEntanglement
{
    using System.Windows.Media.Media3D;

    public class Detector
    {
        private Vector3D direction;


        public Detector(Vector3D direction)
        {
            this.direction = direction;
        }


        public bool InDirectionOfDetector(Particle particle)
        {
            return particle.ExposeToField(this.direction);
        }
    }
}
