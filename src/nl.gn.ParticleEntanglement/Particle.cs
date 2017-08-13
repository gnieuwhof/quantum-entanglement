namespace nl.gn.ParticleEntanglement
{
    using System;
    using System.Windows.Media.Media3D;

    public class Particle
    {
        private Vector3D direction;
        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());
        private readonly object padlock;


        public Particle(Vector3D direction)
        {
            if (direction == null)
                throw new ArgumentNullException(nameof(direction));
            if (direction.Length == 0)
                throw new ArgumentException(
                    "Direction cannot have a length of zero",
                    nameof(direction)
                    );

            this.padlock = new object();

            this.direction = direction;
        }

        public Particle(Particle partner)
        {
            if (partner == null)
                throw new ArgumentNullException(nameof(partner));

            this.padlock = partner.padlock;

            partner.Partner = this;
            this.Partner = partner;
        }


        public Particle Partner
        {
            get;
            private set;
        }


        /*
         * To detect our direction a magnetic field is generated around us.
         * This can have us to emit a photon.
         * Whether we emit a photon depends on the difference in the direction
         * of our spin and the direction of the field of the detector.
         * Basically, the larger the difference the more likely it is that we
         * emit a photon.
         */
        public bool ExposeToField(Vector3D fieldDirection)
        {
            if(fieldDirection.Length == 0)
                throw new ArgumentException(
                    "Field direction cannot have a length of zero",
                    nameof(fieldDirection)
                    );

            double deltaDegrees = Vector3D.AngleBetween(this.direction, fieldDirection);
            
            // For whatever reason the AngleBetween method converts the radians to degrees.
            // So, convert them back.
            double deltaRadians = deltaDegrees * (Math.PI / 180.0);

            double emitProbability = Probability(deltaRadians);
            
            double ran = this.random.NextDouble();

            bool isEmittingPhoton = (ran < emitProbability);

            this.direction = fieldDirection;

            lock (padlock)
            {
                if (this.Partner != null)
                {
                    // We're entangled.
                    Vector3D partnerDirection = this.direction;
                    
                    if (!isEmittingPhoton)
                    {
                        // Opposite direction.
                        partnerDirection.Negate();
                    }

                    // Spooky action!!
                    // Change the direction of the entangled partner.
                    this.Partner.direction = partnerDirection;

                    // Destroy entanglement.
                    this.Partner.Partner = null;
                    this.Partner = null;
                }
            }

            if(isEmittingPhoton)
            {
                this.direction.Negate();
            }

            return isEmittingPhoton;
        }
        
        private static double Probability(double radAngle)
        {
            // 1 - (cos(angle/2))^2

            double result = Math.Pow(Math.Cos(radAngle / 2), 2);

            // We need to round, otherwise PI/2 (90 degrees) could result
            // in equal (instead of opposite) spin
            // because cos(PI/2) equals a very small positive number.
            double rounded = Math.Round(result, 15);

            return (1 - rounded);
        }
    }
}
