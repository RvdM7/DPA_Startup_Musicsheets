using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Domain.Additive
{
    static class AdditiveFactory
    {
        public static Dots getDots(int dots)
        {
            return Dots.create(dots);
        }

        public static Flat getFlat()
        {
            return Flat.create();
        }

        public static Sharp getSharp()
        {
            return Sharp.create();
        }

        public static Flat getFlat(int flats)
        {
            return Flat.create(flats);
        }

        public static Sharp getSharp(int sharps)
        {
            return Sharp.create(sharps);
        }
    }
}
