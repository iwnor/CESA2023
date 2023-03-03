using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStartPoint : MonoBehaviour
{
    [SerializeField] private float lightLineDistance = 1000.0f;  // 光の反射探索距離
    private LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int refrectTimes = 3;  // 反射回数

    private List<Vector3> refrectPositions;

    private bool nowPrismHitFlag;
    private bool oldPrismHitFlag;
    private bool endFlag = false;

    private PrismScript hitPrismScript; // 当たったプリズムのスクリプトを保存しておく（消灯のため）

    // Start is called before the first frame update
    void Start()
    {
        refrectPositions = new List<Vector3>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // リストの初期化
        refrectPositions.Clear();

        // 始点の取得
        refrectPositions.Add(transform.position);
        Vector3 refrectVec = this.gameObject.transform.forward;

        bool lightLineRefrectEndFlag = true;
        while (lightLineRefrectEndFlag)
        {
            // 終点を取得するためにRayを用いて当たる場所を取得
            RaycastHit rayHitObject;
            Physics.Raycast(refrectPositions[refrectPositions.Count - 1], refrectVec, out rayHitObject, lightLineDistance, wallLayer);
            refrectPositions.Add(rayHitObject.point);

            // 鏡に当たった場合反射させる
            if (rayHitObject.collider.tag == "Mirror")
            {
                // 1個目の光のベクトルを取得
                Vector3 beforeLightLineVec = refrectPositions[refrectPositions.Count - 1] - refrectPositions[refrectPositions.Count - 2];

                // 反射光の計算
                refrectVec = Vector3.Reflect(beforeLightLineVec, rayHitObject.normal);
            }
            else
            {
                switch (rayHitObject.collider.tag)
                {
                    // プリズムに当たった場合拡散させる
                    case "Prism":
                        hitPrismScript = rayHitObject.transform.GetComponent<PrismScript>();
                        hitPrismScript.TurnOnPrismLight(refrectPositions[refrectPositions.Count - 1] - refrectPositions[refrectPositions.Count - 2]);
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

                    case "Player":
                        if (endFlag == false)
                        {
                            //GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().ActiveReloadButton();
                            endFlag = true;
                        }
                        break;

                    default:
                        break;
                }

                // 反射しないのでループ終了
                lightLineRefrectEndFlag = false;
            }
        }

        // レーザーの頂点座標の指定
        lineRenderer.positionCount = refrectPositions.Count;
        lineRenderer.SetPositions(refrectPositions.ToArray());

        if (nowPrismHitFlag == false && oldPrismHitFlag == true)
        {
            oldPrismHitFlag = false;
            hitPrismScript.TurnOffPrismLight();
        }
        else if (nowPrismHitFlag == true)
        {
            nowPrismHitFlag = false;
            oldPrismHitFlag = true;
        }
    }

    public bool shotLazer()
    {
        for(int i = 0;i + 1 < refrectPositions.Count;i++)
        {
            Vector3 direction = refrectPositions[i + 1] - refrectPositions[i];
            foreach (RaycastHit hit in Physics.RaycastAll(refrectPositions[i], direction))
            {
                Debug.Log(hit.point);
                if(hit.collider.tag == "Crystal")
                {
                    hit.transform.GetComponent<CrystalScript>().countUpLazer();
                }
                Debug.DrawRay(refrectPositions[i], direction);
            }
        }

        return false;
    }
}
