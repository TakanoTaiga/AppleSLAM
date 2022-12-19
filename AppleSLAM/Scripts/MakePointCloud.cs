using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.IO;

namespace UnityEngine.XR.ARFoundation
{
public class MakePointCloud : MonoBehaviour
{
    public GameObject PointPrefab;

    private ARPointCloud m_PointCloud;
    private Dictionary<ulong, Vector3> m_Points = new Dictionary<ulong, Vector3>();

    ParticleSystem m_ParticleSystem;
    ParticleSystem.Particle[] m_Particles;
    List<Vector3> positions = new List<Vector3>();

    void Awake()
    {
        m_PointCloud = GetComponent<ARPointCloud>();
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        int numParticles = m_Points.Count + positions.Count;
        m_Particles = new ParticleSystem.Particle[numParticles];

        foreach (var kvp in m_Points){
            var vec = kvp.Value;
            bool addFlag = true;

            double scaler = 5f;

            int vecx = (int)(vec.x * scaler);
            int vecy = (int)(vec.y * scaler);
            int vecz = (int)(vec.z * scaler);
            for (int i = 0; i < positions.Count;i++){
                int x = (int)(positions[i].x * scaler);
                int y = (int)(positions[i].y * scaler);
                int z = (int)(positions[i].z * scaler);
                if(x == vecx && y == vecy && z == vecz){
                    addFlag = false;
                    break;
                }
            }
            if(addFlag == true){
                positions.Add(vec);
            }
        }

        //make point cloud.
        for (int i = 0; i < positions.Count;i++)
        {
            m_Particles[i].startColor = m_ParticleSystem.main.startColor.color;
            m_Particles[i].startSize = m_ParticleSystem.main.startSize.constant;
            m_Particles[i].position = positions[i];
            m_Particles[i].remainingLifetime = 0.03f;
        }
        m_ParticleSystem.Clear();
        m_ParticleSystem.SetParticles(m_Particles, numParticles);
    }

    void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
        {
            if (!m_PointCloud.positions.HasValue)
                return;

            var positions = m_PointCloud.positions.Value;

            if (m_PointCloud.identifiers.HasValue)
            {
                var identifiers = m_PointCloud.identifiers.Value;
                for (int i = 0; i < positions.Length; ++i)
                {
                    m_Points[identifiers[i]] = positions[i];
                }
            }
        }

        void OnEnable()
        {
            m_PointCloud.updated += OnPointCloudChanged;
        }

        void OnDisable()
        {
            m_PointCloud.updated -= OnPointCloudChanged;
        }
}
}

