using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStartPoint : MonoBehaviour
{
    [SerializeField] private float lightLineDistance = 100.0f;  // 光の反射探索距離
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 2;
        // 始点の取得
        Vector3 pos1 = this.gameObject.transform.position;

        // 終点を取得するためにRayを用いて当たる場所を取得
        RaycastHit rayHitObject;
        Physics.Raycast(pos1, this.gameObject.transform.forward, out rayHitObject, lightLineDistance);
        Vector3 pos2 = rayHitObject.point;

        // 線の始点、終点の指定
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);

        bool lightLineRefrectEndFlag = true;
        while (lightLineRefrectEndFlag)
        {
            // 鏡に当たった場合反射させる
            if (rayHitObject.collider.tag == "Mirror")
            {
                lineRenderer.positionCount++;
                // 1個目の光のベクトルを取得
                Vector3 beforeLightLineVec = pos2 - pos1;

                // 反射光の計算
                Vector3 reflectVec = Vector3.Reflect(beforeLightLineVec, rayHitObject.normal);
                Physics.Raycast(rayHitObject.transform.position, reflectVec, out rayHitObject, lightLineDistance);

                // 線の始点、終点の指定
                pos1 = pos2;
                pos2 = rayHitObject.point;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos2);
            }
            else
            {
                // 反射しないのでループ終了
                lightLineRefrectEndFlag = false;
            }
        }
    }
}
