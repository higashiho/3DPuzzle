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
        // Start is called before the first frame update
        void Start()
        {
            // 不要なUIを非表示
            ImageFill.OffLoadingImages(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}