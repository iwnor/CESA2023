using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStartPoint : MonoBehaviour
{
    [SerializeField] private float lightLineDistance = 1000.0f;  // ���̔��˒T������
    private LineRenderer lineRenderer;
    [SerializeField] private LayerMask wallLayer;

    private bool nowPrismHitFlag;
    private bool oldPrismHitFlag;
    private bool endFlag = false;

    PrismScript hitPrismScript; // ���������v���Y���̃X�N���v�g��ۑ����Ă����i�����̂��߁j

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 2;
        // �n�_�̎擾
        Vector3 pos1 = this.gameObject.transform.position;

        // �I�_���擾���邽�߂�Ray��p���ē�����ꏊ���擾
        RaycastHit rayHitObject;
        Physics.Raycast(pos1, this.gameObject.transform.forward, out rayHitObject, lightLineDistance, wallLayer);
        Vector3 pos2 = rayHitObject.point;

        // ���̎n�_�A�I�_�̎w��
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);

        bool lightLineRefrectEndFlag = true;
        while (lightLineRefrectEndFlag)
        {
            // ���ɓ��������ꍇ���˂�����
            if (rayHitObject.collider.tag == "Mirror")
            {
                lineRenderer.positionCount++;
                // 1�ڂ̌��̃x�N�g�����擾
                Vector3 beforeLightLineVec = pos2 - pos1;

                // ���ˌ��̌v�Z
                Vector3 reflectVec = Vector3.Reflect(beforeLightLineVec, rayHitObject.normal);
                Physics.Raycast(rayHitObject.transform.position, reflectVec, out rayHitObject, lightLineDistance, wallLayer);

                // ���̎n�_�A�I�_�̎w��
                pos1 = pos2;
                pos2 = rayHitObject.point;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos2);
            }
            else
            {
                switch (rayHitObject.collider.tag)
                {
                    // �v���Y���ɓ��������ꍇ�g�U������
                    case "Prism":
                        hitPrismScript = rayHitObject.transform.GetComponent<PrismScript>();
                        hitPrismScript.TurnOnPrismLight(pos2 - pos1);
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

                    default:
                        break;
                }

                // ���˂��Ȃ��̂Ń��[�v�I��
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
