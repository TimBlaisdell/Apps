using System;

namespace CyclicalAutomata {
   public partial class CyclicalAutomata {
      private partial class Cell {
         public class Props : IEquatable<Props> {
            public Props(int state, int age = 0) {
               Age = age;
               State = state;
            }
            public Props(Props properties) {
               Age = properties.Age;
               State = properties.State;
            }
            public override bool Equals(object obj) {
               if (ReferenceEquals(null, obj)) return false;
               if (ReferenceEquals(this, obj)) return true;
               if (obj.GetType() != GetType()) return false;
               return Equals((Props) obj);
            }
            public bool Equals(Props other) {
               if (ReferenceEquals(null, other)) return false;
               if (ReferenceEquals(this, other)) return true;
               return Age == other.Age && State == other.State;
            }
            public override int GetHashCode() {
               return 0;
            }
            bool IEquatable<Props>.Equals(Props other) {
               return Equals(other);
            }
            public static bool operator ==(Props p1, Props p2) {
               if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null)) return true;
               if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null)) return false;
               return p1.Equals(p2);
            }
            public static bool operator !=(Props p1, Props p2) {
               if (ReferenceEquals(p1, null)) return !ReferenceEquals(p2, null);
               return !p1.Equals(p2);
            }
            public int Age;
            public int State;
         }
      }
   }
}