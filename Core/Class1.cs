using System.Collections.Generic;
using System;
using System.Linq;
using ScottPlot;
using System.Runtime.CompilerServices;

namespace Core
{
    [Serializable]
    public struct Vector : IEnumerable<double>
    {
        public double[] vector { get; set; }
        public int dimension { get; }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            // call the generic version of the method
            return this.GetEnumerator();
        }

        public IEnumerator<double> GetEnumerator()
        {
            foreach (double d in vector)
                yield return d;
        }

        public Vector(int Dimension)
        {
            vector = new double[Dimension];
            dimension = Dimension;
        }
        public void Print()
        {
            Console.Write("[");
            //foreach(double d in this.vector){
            //    Console.Write($"{d} ");
            Console.Write(string.Join(", ", this.AsEnumerable().Select<double, string>(x => x.ToString())));
            Console.WriteLine("]");
        }

        public static Vector fromArray(double[] a)
        {
            Vector r = new Vector(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                r.vector[i] = a[i];
            }
            return r;
        }
        public Vector Upscale(int dimensions)
        {
            if (dimensions < 0) throw new Exception();
            Vector r = new Vector(this.dimension + dimensions);
            int c = 0;
            foreach (double d in this)
            {
                r[c] = this[c];
                c++;  // haha C++
            }
            return r;
        }

        public Vector PutThrough(Func<double, double> a)
        {
            Vector r = new Vector(this.dimension);
            for (int i = 0; i < this.dimension; i++)
            {
                r.vector[i] = a(this.vector[i]);
            }
            return r;
        }

        public void Inset_at_start_Same_Length(double val)
        {
            double[] new_vals = new double[this.dimension];
            vector.Take<double>(this.dimension - 1).ToArray().CopyTo(new_vals, 1);
            new_vals[0] = val;
            this.vector = new_vals;
        }
        public void Insert_at_end_Same_Length(double val)
        {
            for (int i = 0; i < this.dimension - 1; i++)
            {
                this[i] = this[i + 1];
            }
            this[this.dimension - 1] = val;
        }

        public void Add(double val)
        {
            Insert_at_end_Same_Length(val);
        }

        public double Magnitude()
        {
            return Math.Sqrt(this*this);
        }

        public static double operator *(Vector a, Vector b)
        {
            if (a.dimension != b.dimension) throw new Exception("Wrong dimensions");
            double r = 0;
            for (int i = 0; i < a.dimension; i++)
            {
                r += a.vector[i] * b.vector[i];
            }
            return r;
        }// le dot product

        public static Vector operator ^(Vector a, Vector b)
        { //hadamard product
            if (a.dimension != b.dimension) throw new Exception("Wrong dimensions");
            Vector r = new Vector(a.dimension);
            for (int i = 0; i < a.dimension; i++)
            {
                r.vector[i] += a.vector[i] * b.vector[i];
            }
            return r;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            if (a.dimension != b.dimension) throw new Exception("Wrong dimensions");
            Vector r = new Vector(a.dimension);
            for (int i = 0; i < a.dimension; i++)
            {
                r.vector[i] = a.vector[i] + b.vector[i];
            }
            return r;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            if (a.dimension != b.dimension) throw new Exception("Wrong dimensions");
            Vector r = new Vector(a.dimension);
            for (int i = 0; i < a.dimension; i++)
            {
                r.vector[i] = a.vector[i] - b.vector[i];
            }
            return r;
        }
        public static Vector operator *(double a, Vector b)
        {
            Vector r = new Vector(b.dimension);
            for (int i = 0; i < b.dimension; i++)
            {
                r.vector[i] = b.vector[i] * a;
            }
            return r;
        }
        public static Vector operator *(Vector b, double a)
        {
            Vector r = new Vector(b.dimension);
            for (int i = 0; i < b.dimension; i++)
            {
                r.vector[i] = b.vector[i] * a;
            }
            return r;
        }
        public static Vector operator /(Vector b, double a)
        {
            Vector r = new Vector(b.dimension);
            for (int i = 0; i < b.dimension; i++)
            {
                r.vector[i] = b.vector[i] / a;
            }
            return r;
        }

        public static explicit operator int[](Vector v)
        {
            //convert vector to it array
            int[] r = new int[v.dimension];
            for (int i = 0; i < v.dimension; i++)
            {
                r[i] = (int)v[i];
            }
            return r;
        }

        public double this[int i]
        {
            get
            {
                return this.vector[i];
            }
            set
            {
                this.vector[i] = value;
            }
        }

        public static implicit operator Coordinates(Vector v) => new Coordinates(v[0], v[1]); 

    }
}