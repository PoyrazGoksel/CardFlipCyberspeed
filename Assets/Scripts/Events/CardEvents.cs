using UnityEngine.Events;
using Views;

namespace Events
{
    public static class CardEvents
    {
        public static UnityAction<ICard> CardOpened;
        public static UnityAction PreCardOpen;
    }
}