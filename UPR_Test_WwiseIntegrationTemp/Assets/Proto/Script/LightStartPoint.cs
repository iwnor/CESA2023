using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStartPoint : MonoBehaviour
{
    [SerializeField] private float lightLineDistance = 1000.0f;  // 光の反射探索距離
    private LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;

    private bool nowPrismHitFlag;
    private bool oldPrismHitFlag;
    private bool endFlag = false;

    PrismScript hitPrismScript; // 当たったプリズムのスクリプトを保存しておく（消灯のため）

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 2;
        // 始点の取得
        Vector3 pos1 = this.gameObject.transform.position;

        // 終点を取得するためにRayを用いて当たる場所を取得
        RaycastHit rayHitObject;
        Physics.Raycast(pos1, this.gameObject.transform.forward, out rayHitObject, lightLineDistance, wallLayer);
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
                Physics.Raycast(rayHitObject.transform.position, reflectVec, out rayHitObject, lightLineDistance, wallLayer);

                // 線の始点、終点の指定
                pos1 = pos2;
                pos2 = rayHitObject.point;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos2);
            }
            else
            {
                switch (rayHitObject.collider.tag)
                {
                    // プリズムに当たった場合拡散させる
                    case "Prism":
                        hitPrismScript = rayHitObject.transform.GetComponent<PrismScript>();
                        hitPrismScript.TurnOnPrismLight(pos2 - pos1);
                        nowPrismHitFlag = true;
                        break;

                    // ゴールに当たった場合クリア扱いにする
                    case "Goal":
                        if (endFlag == false)
                        {
                            GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().ActiveReloadButton();
                            endFlag = true;
                        }
                        break;

                    default:
                        break;
                }

                // 反射しないのでループ終了
                lightLineRefrectEndFlag = false;
            }

            if(nowPrismHitFlag == false && oldPrismHitFlag == true)
            { 
                oldPrismHitFlag = false;
                hitPrismScript.TurnOffPrismLight();
            }
            else if(nowPrismHitFlag == true)
            {
                nowPrismHitFlag = false;
                oldPrismHitFlag = true;
            }
        }
    }
}
