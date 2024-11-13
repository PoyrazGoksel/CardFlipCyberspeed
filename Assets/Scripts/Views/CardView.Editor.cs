#if UNITY_EDITOR
using UnityEngine;

namespace Views
{
    public partial class CardView : ICardEditor
    {
        void ICardEditor.SetID(int id)
        {
            _id = id;
        }

        void ICardEditor.SetPos(Vector3 wPos)
        {
            _initPos = wPos;
            _transform.position = _initPos;
        }

        void ICardEditor.SetCoords(Vector2Int coords)
        {
            _coords = coords;
        }

        public CardView GetConcreteType() => this;

        public void SetParent(Transform trans)
        {
            _transform.parent = trans;
        }
    }

    public interface ICardEditor
    {
        void SetID(int id);

        void SetPos(Vector3 wPos);

        void SetCoords(Vector2Int coords);

        CardView GetConcreteType();
        
        GameObject gameObject{get;}

        void SetParent(Transform trans);
        
        Bounds ColliderBounds{get;}
    }
}
#endif