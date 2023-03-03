using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStartPoint : MonoBehaviour
{
    [SerializeField] private float lightLineDistance = 1000.0f;  // ���̔��˒T������
    private LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int refrectTimes = 3;  // ���ˉ�

    private List<Vector3> refrectPositions;

    private bool nowPrismHitFlag;
    private bool oldPrismHitFlag;
    private bool endFlag = false;

    private PrismScript hitPrismScript; // ���������v���Y���̃X�N���v�g��ۑ����Ă����i�����̂��߁j

    // Start is called before the first frame update
    void Start()
    {
        refrectPositions = new List<Vector3>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���X�g�̏�����
        refrectPositions.Clear();

        // �n�_�̎擾
        refrectPositions.Add(transform.position);
        Vector3 refrectVec = this.gameObject.transform.forward;

        bool lightLineRefrectEndFlag = true;
        while (lightLineRefrectEndFlag)
        {
            // �I�_���擾���邽�߂�Ray��p���ē�����ꏊ���擾
            RaycastHit rayHitObject;
            Physics.Raycast(refrectPositions[refrectPositions.Count - 1], refrectVec, out rayHitObject, lightLineDistance, wallLayer);
            refrectPositions.Add(rayHitObject.point);

            // ���ɓ��������ꍇ���˂�����
            if (rayHitObject.collider.tag == "Mirror")
            {
                // 1�ڂ̌��̃x�N�g�����擾
                Vector3 beforeLightLineVec = refrectPositions[refrectPositions.Count - 1] - refrectPositions[refrectPositions.Count - 2];

                // ���ˌ��̌v�Z
                refrectVec = Vector3.Reflect(beforeLightLineVec, rayHitObject.normal);
            }
            else
            {
                switch (rayHitObject.collider.tag)
                {
                    // �v���Y���ɓ��������ꍇ�g�U������
                    case "Prism":
                        hitPrismScript = rayHitObject.transform.GetComponent<PrismScript>();
                        hitPrismScript.TurnOnPrismLight(refrectPositions[refrectPositions.Count - 1] - refrectPositions[refrectPositions.Count - 2]);
                        nowPrismHitFlag = true;
                        break;

                    // �S�[���ɓ��������ꍇ�N���A�����ɂ���
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

                // ���˂��Ȃ��̂Ń��[�v�I��
                lightLineRefrectEndFlag = false;
            }
        }

        // ���[�U�[�̒��_���W�̎w��
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
