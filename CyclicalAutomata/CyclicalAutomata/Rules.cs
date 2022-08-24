namespace CyclicalAutomata {
   public partial class CyclicalAutomata {
      public enum Rules {
         NextOnly = 0,
         PrevOnly = 1,
         NextIfDouble = 2,
         NextIfTriple = 3,
         NextIfQuad = 4,
         NextIfQuint = 5,
         NextOrPrev,
         FavorNext,
         NextIfMore,
         NextIfZero,
         NextProgressive,
         NextFavorDiagonal,
         NextFavorOrthogonal,
         NextRotatingFavor,
      }
   }
}