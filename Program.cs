using System;

namespace QuaternionStudy
{
    class Program
    {
        static void Main()
        {
            var v = new double[] { 1, 0, 0 };
            for (var i = 0; i < 360; i += 10)
            {
                var q = Quaternion.RotationQ(0, 0, 10, i * Math.PI / 180);
                var v2 = q.Rotate(v);
                Console.WriteLine("Degree={0:d3} => ({1:f4}, {2:f4}, {3:f4})", i, v2[0], v2[1], v2[2]);
            }
            Console.ReadLine();
        }
    }
}
