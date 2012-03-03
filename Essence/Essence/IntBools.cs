using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essence {
	public struct IntBools {

		public IntBools(int val) {
			bits = val;
		}

		public bool this[int index] {
			get {
				return (bits & (1 << index)) != 0;
			}
			set {
				if(value) bits |= (1 << index);
				else bits &= ~(1 << index);
			}
		}

		private int bits;
		public int Value {
			get {
				return bits;
			}
		}
	}
}
