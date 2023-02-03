using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoadingImage
{
    public class LoadingImageController : BaseLoadingImage
    {
        public static BaseLoadingImage tmpImage{get; set;}
        
        void Awake()
        {
            // 複数の生成したら破棄
            if(tmpImage != null)
            {
                Destroy(this);
                return;
            }
            tmpImage = this;
            DontDestroyOnLoad(this);
        }
        // Start is called before the first frame update
        void Start()
        {
            Caircles = GetComponentsInChildren<Image>();
            LoadingImages.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}