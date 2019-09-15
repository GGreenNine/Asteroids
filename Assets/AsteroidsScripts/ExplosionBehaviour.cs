using Asteroids;
using HappyUnity.TransformUtils;
using UnityEngine;

namespace DefaultNamespace
{
    public class ExplosionBehaviour : AsteroidsGameBehaviour
    {
        ParticleSystem m_explosionPS;
        AudioSource m_explosionAudio;

        void Awake()
        {
            m_explosionPS = GetComponent<ParticleSystem>();
            m_explosionAudio = GetComponent<AudioSource>();
        }

        void OnEnable()
        {
            m_explosionPS.Play();
            m_explosionAudio.Play();
            InvokeRemoveFromGame(m_explosionPS.startLifetime);
        }

        protected override void RequestDestruction()
        {
            gameObject.SetActive(false);
        }
    }
}

