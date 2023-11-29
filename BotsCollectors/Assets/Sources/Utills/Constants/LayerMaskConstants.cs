using UnityEngine;

namespace Sources.Utills.Constants
{
    public static class LayerMaskConstants
    {
        //TODO как узнать номер лейера?
        public const int Default = 0;

        //TODO правильно ли записал?
        private static readonly int _touchableLayer = LayerMask.NameToLayer("Touchable");
        public static readonly int Touchable = 1 << _touchableLayer;
        
        private static readonly int _obstacleLayer = LayerMask.NameToLayer("Obstacle");
        //TODO возможно тут нужно поставиить ноль
        public static readonly int Obstacle = 1 << _obstacleLayer;

    }
}