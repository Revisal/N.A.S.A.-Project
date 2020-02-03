using System;

namespace Friction
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables needed for friction 
            const double MOON_MASS = 7.34767309E+22;
            const double MOON_GRAV_ACC = 1.62;
            double degree = 0;

            // Allows the user to enter an angle in that is less than 10
            Console.Write("Enter the angle of launch(Must be less than 10 degrees): ");
            degree = Convert.ToDouble(Console.ReadLine());

            while (degree <= 0)
            {
                Console.Write("Invalid input, try again: ");
                degree = Convert.ToDouble(Console.ReadLine());
            }

            // Calculates normal force 
            double normalForce = MOON_MASS * MOON_GRAV_ACC;

            // Converts from radians to degrees
            double newAngle = (degree * (Math.PI)) / 180;

            // Calculates tangential force
            double tangentialForce = normalForce * Math.Sin(newAngle);

            // Calculates friction
            double friction = tangentialForce / normalForce;

            // Displays friction
            Console.Write("Friction on the moon is: " + friction);
        }
    }
}
