namespace UnitTests
{
    using nl.gn.ParticleEntanglement;
    using NUnit.Framework;
    using System;
    using System.Windows.Media.Media3D;

    [TestFixture]
    public class DetectorTests
    {
        [Test]
        public void RandomFiftyFiftyTest()
        {
            Vector3D angle = VectorFactory.Create(0);
            var detector = new Detector(angle);

            int count = 0;

            int iterationCount = 1000;
            for (int i = 0; i < iterationCount; ++i)
            {
                var particle = new Particle(VectorFactory.Create());

                if (detector.InDirectionOfDetector(particle))
                {
                    ++count;
                }
            }

            int expected = (int)(iterationCount * 0.50);
            int margin = (int)(iterationCount * 0.05);

            Assert.AreEqual(count, expected, margin);
        }

        [Test]
        public void FixedFiftyFiftyTest()
        {
            Vector3D detectorAngle = VectorFactory.Create(0);
            Vector3D particleAngle = VectorFactory.Create(90);
            var detector = new Detector(detectorAngle);

            int count = 0;

            int iterationCount = 1000;
            for (int i = 0; i < iterationCount; ++i)
            {
                var particle = new Particle(VectorFactory.Create());

                // Force the particle direction.
                particle.ExposeToField(particleAngle);

                if (detector.InDirectionOfDetector(particle))
                {
                    ++count;
                }
            }

            int expected = (int)(iterationCount * 0.50);
            int margin = (int)(iterationCount * 0.05);

            Assert.AreEqual(expected, count, margin);
        }

        [Test]
        public void OppositeDirectionTest()
        {
            Vector3D angle = VectorFactory.Create(0);
            var detector = new Detector(angle);

            for (int i = 0; i < 1000; ++i)
            {
                var particle1 = new Particle(VectorFactory.Create());
                var particle2 = new Particle(particle1);

                bool up1 = detector.InDirectionOfDetector(particle1);
                bool up2 = detector.InDirectionOfDetector(particle2);

                Assert.AreNotEqual(up1, up2);
            }
        }

        [Test]
        public void SixtyDegreesTest()
        {
            Vector3D detectorAngle = VectorFactory.Create(60);
            var detector = new Detector(detectorAngle);

            int count = 0;

            int iterationCount = 1000;
            for (int i = 0; i < iterationCount; ++i)
            {
                Vector3D particleAngle = VectorFactory.Create(180);
                var particle = new Particle(particleAngle);

                bool up = detector.InDirectionOfDetector(particle);

                if (up)
                {
                    ++count;
                }
            }

            int expected = (int)(iterationCount * 0.75);
            int margin = (int)(iterationCount * 0.05);

            Assert.AreEqual(expected, count, margin);
        }

        [Test]
        public void FortyFiveDegreesTest()
        {
            var detector1 = new Detector(VectorFactory.Create(0));
            var detector2 = new Detector(VectorFactory.Create(45));

            int count = 0;

            int iterationCount = 1000;
            for (int i = 0; i < iterationCount; ++i)
            {
                var particle1 = new Particle(VectorFactory.Create());
                var particle2 = new Particle(particle1);

                bool up1 = detector1.InDirectionOfDetector(particle1);
                bool up2 = detector2.InDirectionOfDetector(particle2);

                if (up1 != up2)
                {
                    ++count;
                }
            }

            int expected = (int)(iterationCount * 0.85);
            int margin = (int)(iterationCount * 0.05);

            Assert.AreEqual(expected, count, margin);
        }

        [Test]
        // @see EPR paradox.
        public void JohnBellTest()
        {
            int count = 0;
            int iterationCount = 1000;
            var random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < iterationCount; ++i)
            {
                var particle1 = new Particle(VectorFactory.Create());
                var particle2 = new Particle(particle1);

                var detector1 = new Detector(VectorFactory.Create(random.Next(3) * 120));
                var detector2 = new Detector(VectorFactory.Create(random.Next(3) * 120));

                bool up1 = detector1.InDirectionOfDetector(particle1);
                bool up2 = detector2.InDirectionOfDetector(particle2);

                if (up1 != up2)
                {
                    ++count;
                }
            }

            int expected = (int)(iterationCount * 0.50);
            int margin = (int)(iterationCount * 0.05);

            Assert.AreEqual(expected, count, margin);
        }
    }
}
