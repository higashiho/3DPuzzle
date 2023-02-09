using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;
using button;

namespace LoadingImage
{
    public class LoadingImageController : BaseLoadingImage
    {
        
        
        void Awake()
        {
            // 複数の生成したら破棄
            if(tmpImage != null)
            {
                Destroy(this);
                return;
            }
            tmpImage = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            // 不要なUIを非表示
            ImageFill.OffLoadingImages(tmpImage);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}