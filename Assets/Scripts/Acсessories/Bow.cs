using System;
using System.Linq;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public static float DynamicFOV;
    float AddFOV;
    [SerializeField] Transform bowpoint1, bowpoint2, launcher,
        arrowpoint_1, arrowpoint_2, arrowpoint_3, quiver;
    [SerializeField] GameObject Arrow;
    GameObject arrow1, arrow2, arrow3;
    Vector3 arrowscale;
    public string stage = "������0";
    float edge, timer = .2f;
    float dif = 0;
    Vector2[] diffs;
    bool bowAllowed = true;

    void Start()
    {
        edge = FingerPosition.edge;
        arrowscale = Arrow.transform.lossyScale;
    }

    void Update()
    {
        Vector2[] curDiffs = diffs;
        diffs = new Vector2[]{ FingerPosition.dif1, FingerPosition.dif2 };
        float curDif = dif;
        dif = FingerPosition.distance;
        if (dif != curDif && dif == 1 && new string[] {"�������", "�������", "�������"}.Contains(stage))
        {
            timer -= Time.deltaTime;
            if (timer <= 0) timer = .2f;
            else
            {
                dif = curDif;
                diffs = curDiffs;
            }
        }
        else timer = .2f;

        if (!bowAllowed) stage = "����������";
        else stage = TensionStage(dif, edge, stage, Mathf.Abs(diffs[0].y) + Mathf.Abs(diffs[1].y),
            Mathf.Abs(diffs[0].y + diffs[1].y));

        
        switch (stage)
        {
            case "����������":
            AddFOV = 0;
            GetComponent<Interpolator>().Target = quiver;
            Destroy(gameObject, .2f);
            break;
            
            case "���������":
            AddFOV = -Mathf.Sqrt(-dif) * .75f;
            break;
            case "������2-���������":
            stage = "���������";
            break;
            case "�������-���������":
            stage = "���������";
            break;

            case "������0":
            AddFOV = 0;
            break;
            case "�������-������0":
            GetComponent<Interpolator>().Target = bowpoint1;
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Interpolator>().Smoothness = 1;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            stage = "������0";
            break;
            case "�������-������0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow2.GetComponent<Arrow>().Shoot();
            arrow3.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            arrow2.GetComponent<Interpolator>().enabled = false;
            arrow3.GetComponent<Interpolator>().enabled = false;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            arrow2.GetComponent<Arrow>().Disappear(quiver);
            arrow3.GetComponent<Arrow>().Disappear(quiver);
            stage = "������0";
            break;
            case "�������-������0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            arrow1.GetComponent<Rigidbody>().useGravity = false;
            Time.timeScale = 1;
            stage = "������0";
            break;
            case "�������-������0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            stage = "������0";
            break;
            case "������1-������0":
            stage = "������0";
            break;
            case "������2-������0":
            stage = "������0";
            break;

            case "������1":
            AddFOV = 0;
            break;
            case "������0-������1":
            stage = "������1";
            break;
            case "���������-������1":
            stage = "������1";
            break;
            case "������2-������1":
            stage = "������1";
            break;

            case "������2":
            AddFOV = Mathf.Sqrt(dif) * .75f;
            break;
            case "������1-������2":
            stage = "������2";
            break;
            case "������0-������2":
            stage = "������2";
            break;
            case "���������-������2":
            stage = "������2";
            break;
            case "�������-������2":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;

            arrow1 = Instantiate(Arrow, launcher);
            arrow1.transform.position = quiver.position;
            arrow1.GetComponent<Interpolator>().Target = arrowpoint_1;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            arrowpoint_1.localPosition = new Vector3(0, 0, 0);
            stage = "�����ب������������������";//!!!
            break;
            case "�������-������2":
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            arrow2.GetComponent<Arrow>().Disappear(quiver);
            arrow3.GetComponent<Arrow>().Disappear(quiver);
            stage = "������2";
            break;
            case "�������-������2":
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            stage = "������2";
            break;

            case "�������":
            AddFOV = Mathf.Sqrt(dif) * .75f;
            arrowpoint_1.localPosition = new Vector3(0, 0, -(dif - edge) * .001f);
            break;
            case "������2-�������":
            arrow1 = Instantiate(Arrow, launcher);
            arrow1.transform.position = quiver.position;
            arrow1.GetComponent<Interpolator>().Target = arrowpoint_1;
            stage = "�������";
            break;
            case "�������-�������":
            stage = "�������";
            break;
            case "�������-�������":
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "�������";
            break;
            case "�������-�������":
            Time.timeScale = 1;
            arrow1.transform.localScale = arrowscale;
            stage = "�������";
            break;

            case "�������":
            AddFOV = Mathf.Sqrt(dif);
            arrowpoint_1.localPosition = new Vector3(0, 0, -(dif - edge) * .001f);
            break;
            case "�������-�������":
            Time.timeScale = .75f;
            arrow1.transform.localScale = arrowscale * 1.25f;
            stage = "�������";
            break;
            case "�������-�������":
            Time.timeScale = .75f;
            arrow1.transform.localScale = arrowscale * 1.25f;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "�������";
            break;
            case "�������-�������":
            stage = "�������";
            break;

            case "�������":
            float xDif = Mathf.Abs(diffs[0].x) + Mathf.Abs(diffs[1].x);
            AddFOV = Mathf.Sqrt(xDif) * .1f;
            xDif = Mathf.Clamp(xDif, edge, float.PositiveInfinity);
            AddFOV = Mathf.Sqrt(dif) * .5f;

            arrowpoint_2.transform.localRotation = Quaternion.Euler(0, (xDif - edge) * .125f, 0);
            arrowpoint_3.transform.localRotation = Quaternion.Euler(0, (-xDif + edge) * .125f, 0);

            arrowpoint_1.localPosition = new Vector3(0, 0, -(dif - edge) * .0001f);
            arrowpoint_2.localPosition = new Vector3(xDif * .001f, 0, -(dif - edge) * .00009f);
            arrowpoint_3.localPosition = new Vector3(-xDif * .001f, 0, - (dif - edge) * .00009f);
            break;
            case "�������-�������":
            arrow1.transform.localScale = arrowscale;
            Time.timeScale = 1;

            arrow2 = Instantiate(Arrow, launcher);
            arrow2.transform.position = launcher.position;
            arrow2.GetComponent<Interpolator>().Target = arrowpoint_2;

            arrow3 = Instantiate(Arrow, launcher);
            arrow3.transform.position = launcher.position;
            arrow3.GetComponent<Interpolator>().Target = arrowpoint_3;
            stage = "�������";
            break;
            case "�������-�������":
            arrow2 = Instantiate(Arrow, launcher);
            arrow2.transform.position = launcher.position;
            arrow2.GetComponent<Interpolator>().Target = arrowpoint_2;

            arrow3 = Instantiate(Arrow, launcher);
            arrow3.transform.position = launcher.position;
            arrow3.GetComponent<Interpolator>().Target = arrowpoint_3;
            stage = "�������";
            break;
            case "������2-�������":
            stage = "�������";
            break;
            case "�������-�������":
            stage = "�������";
            break;

            case "�������":
            AddFOV = -3;
            PlayerController.Modifiers += .25f;
            arrowpoint_1.localPosition = new Vector3(0, 0, Mathf.Lerp(arrowpoint_1.localPosition.z, -.1f, .05f));
            break;
            case "�����ب������������������":
            AddFOV = -3;
            PlayerController.Modifiers += .25f;
            arrowpoint_1.localPosition = new Vector3(0, 0, Mathf.Lerp(arrowpoint_1.localPosition.z, -.1f, .05f));
            break;
            case "�������-�������":
            GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            stage = "�������";
            break;
            case "�������-�������":
            Time.timeScale = 1;
            arrow1.transform.localScale = arrowscale;
            gameObject.GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            stage = "�������";
            break;
            case "�������-�������":
            GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "�������";
            break;
        }

        DynamicFOV = Mathf.Lerp(DynamicFOV, AddFOV, .5f);

    }

    string TensionStage(float dif, float edge, string curStage, float yModDif, float yCommonDif)
    {
        if (dif < 0)
        {
            return curStage switch
            {
                "������2" => "������2-���������",
                "�������" => "�������-���������",
                "�������" => "�������",//!
                "���������" => "���������",
                "�����ب������������������" => "�����ب������������������",
                _ => "���������"
            } ;
        }
        else if (dif == 0)
        {
            return curStage switch
            {
                "�������" => "�������-������0",
                "�������" => "�������-������0",
                "�������" => "�������-������0",
                "�������" => "�������-������0",
                "������1" => "������1-������0",
                "������2" => "������2-������0",
                "������0" => "������0",
                _ => "������0"
            };
        }
        else if (dif == 1)
        {
            return curStage switch
            {
                "������0" => "������0-������1",
                "���������" => "���������-������1",
                "�������" => "�������",
                "�������" => "�������-�������",
                "�������" => "�������-�������",
                "�������" => "�������-�������",
                "������2" => "������2-������1",
                "������1" => "������1",
                "�����ب������������������" => "�������",
                _ => "������1-������"
            };
        }
        else if (yModDif > edge && yModDif >= yCommonDif)
        {
            return curStage switch
            {
                "�������" => "�������-�������",
                "�������" => "�������-�������",
                "������2" => "������2-�������",
                "�������" => "�������",
                "�����ب������������������" => "�����ب������������������",
                "�������" => "�������",
                _ => "�������"
            };
        }
        else if (dif > 1 && dif <= edge)
        {
            return curStage switch
            {
                "������1" => "������1-������2",
                "������0" => "������0-������2",
                "���������" => "���������-������2",
                "�������" => "�������-������2",
                "�������" => "�������-������2",
                "�������" => "�������-������2",
                "������2" => "������2",
                "�����ب������������������" => "�����ب������������������",
                _ => "������2"
            };
        }
        else if (dif > edge && dif <= edge * 3)
        {
            return curStage switch
            {
                "������2" => "������2-�������",
                "�������" => "�������",
                "�����ب������������������" => "�����ب������������������",
                "�������" => "�������-�������",
                "�������" => "�������-�������",
                "�������" => "�������",
                _ => "�������"
            };
        }
        else if (dif > edge * 2)
        {
            return curStage switch
            {
                "�������" => "�������-�������",
                "�������" => "�������-�������",
                "�������" => "�������",
                "�����ب������������������" => "�����ب������������������",
                "�������" => "�������",
                _ => "�������"
            };
        }
        else return stage + "-������";
    }

}
