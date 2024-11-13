using UnityEngine;

namespace Components
{
    public partial class Card : ICardEditor
    {
        public void SetID(int id)
        {
            _id = id;
        }
    }

    public interface ICardEditor
    {
        void SetID(int id);
    }
}