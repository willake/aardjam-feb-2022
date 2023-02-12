using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class Rain : MonoBehaviour
    {
        [Header("Settings")]
        public float emissionRate = 10f;

        private ParticleSystem _particle;

        private ParticleSystem GetParticleSystem()
        {
            if (_particle == null)
            {
                _particle = GetComponent<ParticleSystem>();
            }

            return _particle;
        }

        public void StartRaining()
        {
            var emission = GetParticleSystem().emission;
            emission.rateOverTime = emissionRate;
        }

        public void StopRaining()
        {
            var emission = GetParticleSystem().emission;
            emission.rateOverTime = 0;
        }
    }
}