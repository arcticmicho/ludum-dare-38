using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameModules
{
    public class UIPanelPool : MonoBehaviour
    {
        private Dictionary<Type, Queue<UIPanel>> m_pools = new Dictionary<Type, Queue<UIPanel>>();

        [SerializeField]
        private int m_maxInstancesPerPool = 3;

        public void Clear()
        {
            foreach (var pool in m_pools)
            {
                Queue<UIPanel> queue = pool.Value;

                while (queue.Count > 0)
                {
                    UIPanel panel = queue.Dequeue();
                    Destroy(panel.gameObject);
                }
            }

            m_pools.Clear();

        }

        private UIPanel CreateInstance(UIPanel model)
        {
            GameObject newPanelGO = GameObject.Instantiate(model.gameObject);

            if (newPanelGO != null)
            {
                UIPanel newPanel = newPanelGO.GetComponent<UIPanel>();
                newPanel.PoolCreate(this);
                return newPanel;
            }
            return null;
        }

        public void PreloadInstance(UIPanel model)
        {
            UIPanel newPanel = CreateInstance(model);
            ReleaseInstance(newPanel);
        }

        public UIPanel GetInstance(UIPanel model)
        {
            Queue<UIPanel> queue;

            if (m_pools.TryGetValue(model.GetType(), out queue))
            {
                if (queue.Count > 0)
                {
                    return queue.Dequeue();
                }
            }

            return CreateInstance(model);
        }

        public void ReleaseInstance(UIPanel panel)
        {
            Queue<UIPanel> queue;

            if (m_pools.TryGetValue(panel.GetType(), out queue))
            {
                panel.gameObject.SetActive(false);
                if (queue.Count < m_maxInstancesPerPool)
                {
                    panel.transform.SetParent(transform, false);
                    queue.Enqueue(panel);
                }
                else
                {
                    Destroy(panel.gameObject);
                }
            }
            else
            {
                queue = new Queue<UIPanel>();
                m_pools.Add(panel.GetType(), queue);

                panel.gameObject.SetActive(false);
                panel.transform.SetParent(transform, false);
                queue.Enqueue(panel);
            }
        }
    }
}
