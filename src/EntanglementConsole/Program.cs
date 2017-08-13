namespace EntanglementConsole
{
    using nl.gn.ParticleEntanglement;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Media.Media3D;

    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Note: If both detectors have the same angle
             * the spin of the entangled particles
             * will be opposite 100% of the time.
             * 
             * If the detectors are opposite the spin
             * will be equal 100% of the time.
             * 
             * But, and this the point;
             * if the difference between the detectors
             * is 45 degrees (PI/4), the spin
             * will be opposite 85% of the time.
             * (not 75 as classical physics predicts)
             */

            var d1 = new Detector(new Vector3D(1, 0, 0));
            var d2 = new Detector(new Vector3D(0, 1, 0));

            int diffs = 0;

            for (int i = 0; i < 10000; ++i)
            {
                var p1 = new Particle(VectorFactory.Create());
                var p2 = new Particle(p1);

                bool up1 = false, up2 = false;

                bool async = true;
                if (async)
                {
                    Parallel.Invoke(
                        () => Detect(d1, p1, out up1),
                        () => Detect(d2, p2, out up2)
                        );
                }
                else
                {
                    Detect(d1, p1, out up1);
                    Detect(d2, p2, out up2);
                }

                Console.WriteLine(up1);
                Console.WriteLine(up2);

                if (up1 != up2)
                {
                    ++diffs;
                }

                Console.WriteLine();
            }

            Console.WriteLine(diffs);

            Console.ReadKey();
        }

        private static void Detect(Detector detector, Particle particle, out bool r)
        {
            r = detector.InDirectionOfDetector(particle);
        }
    }
}
