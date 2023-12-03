using UnityEngine;

namespace Sources.Domain.Constants
{
    public static class LayerMaskConstants
    {
        public const int Default = 0;

        public static readonly int TouchableLayer = LayerMask.NameToLayer("Touchable");
        public static readonly int Touchable = 1 << TouchableLayer;

        private static readonly int _obstacleLayer = LayerMask.NameToLayer("Obstacle");
        public static readonly int Obstacle = 1 << _obstacleLayer;

    }
}