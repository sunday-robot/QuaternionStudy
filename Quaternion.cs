using System;

namespace QuaternionStudy
{
    /// <summary>
    /// 以下のページの説明が分かりやすかった。
    /// https://manabitimes.jp/math/983
    /// </summary>
    public class Quaternion
    {
        /// <summary>実部</summary>
        readonly double r;
        /// <summary>虚部i</summary>
        readonly double i;
        /// <summary>虚部j</summary>
        readonly double j;
        /// <summary>虚部k</summary>
        readonly double k;

        /// <param name="r">実部</param>
        /// <param name="i">虚部i</param>
        /// <param name="j">虚部j</param>
        /// <param name="k">虚部k</param>
        public Quaternion(double r, double i, double j, double k)
        {
            this.r = r;
            this.i = i;
            this.j = j;
            this.k = k;
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            var rr = a.r * b.r;
            var ri = a.r * b.i; // i
            var rj = a.r * b.j; // j
            var rk = a.r * b.k; // k

            var ir = a.i * b.r; // i
            var ii = a.i * b.i; // -1
            var ij = a.i * b.j; // k
            var ik = a.i * b.k; // -j

            var jr = a.j * b.r; // j
            var ji = a.j * b.i; // -k
            var jj = a.j * b.j; // -1
            var jk = a.j * b.k; // i

            var kr = a.k * b.r; // k
            var ki = a.k * b.i; // j
            var kj = a.k * b.j; // -i
            var kk = a.k * b.k; // -1

            var r = rr - ii - jj - kk;
            var i = ri + ir + jk - kj;
            var j = rj - ik + jr + ki;
            var k = rk + ij - ji + kr;

            return new Quaternion(r, i, j, k);
        }

        public static Quaternion operator /(Quaternion a, double b)
        {
            return new Quaternion(a.r / b, a.i / b, a.j / b, a.k / b);
        }

        /// <returns>共役(虚数部の符号を反転させたもの)</returns>
        public Quaternion Conjugate => new Quaternion(r, -i, -j, -k);

        double Norm2 => r * r + i * i + j * j + k * k;

        /// <returns>ノルム(ベクトルの長さ)</returns>
        public double Norm => Math.Sqrt(Norm2);

        /// <returns>逆数</returns>
        /// <remarks>回転用に特化した四元数の場合、共役と同じ</remarks>
        public Quaternion Inverse => Conjugate / Norm2;

        /// <summary>
        /// 3次元ベクトルを回転させたものを返す。
        /// </summary>
        /// <param name="v">回転させたい3次元ベクトル</param>
        /// <returns>回転後の3次元ベクトル</returns>
        public double[] Rotate(double[] v)
        {
            var q = this * new Quaternion(0, v[0], v[1], v[2]) * Conjugate;
            return new double[] { q.i, q.j, q.k };
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", r, i, j, k);
        }

        /// <summary>
        /// 回転用に特化させた四元数を生成する。
        /// </summary>
        /// <param name="x">回転軸</param>
        /// <param name="y">回転軸</param>
        /// <param name="z">回転軸</param>
        /// <param name="theta">回転角度</param>
        /// <returns>四元数</returns>
        public static Quaternion RotationQ(double x, double y, double z, double theta)
        {
            var l = Math.Sqrt(x * x + y * y + z * z);
            var c = Math.Cos(theta / 2);
            var s = Math.Sin(theta / 2);
            return new Quaternion(c, x * s / l, y * s / l, z * s / l);
        }
    }
}
