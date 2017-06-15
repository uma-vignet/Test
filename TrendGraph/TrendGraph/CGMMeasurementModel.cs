using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TrendGraph
{
    class CGMMeasurementModel
    {
        public enum TrendInfo : int
        {
            None = -15,
            Min = -2,
            BelowZero = -1,
            Zero = 0,
            AboveZero = 1,
            Max = 2,
        }

        public int SessionId { get; set; }

        public long OffsetTime { get; set; }

        public decimal CGMValue { get; set; }

        /*public TrendValue TrendRaw { get; set; }

        public TrendInfo Trend
        {
            get
            {
                TrendInfo ret = TrendInfo.None;
                if (TrendRaw != null)
                {
                    ret = TrendRaw.Trend;
                }

                return ret;
            }
        }*/
    }
}