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
    public string stage = "мхвецн0";
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
        if (dif != curDif && dif == 1 && new string[] {"нашвмши", "яхкэмши", "рпнимни"}.Contains(stage))
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

        if (!bowAllowed) stage = "ксйнрнапюм";
        else stage = TensionStage(dif, edge, stage, Mathf.Abs(diffs[0].y) + Mathf.Abs(diffs[1].y),
            Mathf.Abs(diffs[0].y + diffs[1].y));

        
        switch (stage)
        {
            case "ксйнрнапюм":
            AddFOV = 0;
            GetComponent<Interpolator>().Target = quiver;
            Destroy(gameObject, .2f);
            break;
            
            case "нрдюкемхе":
            AddFOV = -Mathf.Sqrt(-dif) * .75f;
            break;
            case "мхвецн2-нрдюкемхе":
            stage = "нрдюкемхе";
            break;
            case "рпнимни-нрдюкемхе":
            stage = "нрдюкемхе";
            break;

            case "мхвецн0":
            AddFOV = 0;
            break;
            case "ашярпши-мхвецн0":
            GetComponent<Interpolator>().Target = bowpoint1;
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Interpolator>().Smoothness = 1;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            stage = "мхвецн0";
            break;
            case "рпнимни-мхвецн0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow2.GetComponent<Arrow>().Shoot();
            arrow3.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            arrow2.GetComponent<Interpolator>().enabled = false;
            arrow3.GetComponent<Interpolator>().enabled = false;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            arrow2.GetComponent<Arrow>().Disappear(quiver);
            arrow3.GetComponent<Arrow>().Disappear(quiver);
            stage = "мхвецн0";
            break;
            case "яхкэмши-мхвецн0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            arrow1.GetComponent<Rigidbody>().useGravity = false;
            Time.timeScale = 1;
            stage = "мхвецн0";
            break;
            case "нашвмши-мхвецн0":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;
            stage = "мхвецн0";
            break;
            case "мхвецн1-мхвецн0":
            stage = "мхвецн0";
            break;
            case "мхвецн2-мхвецн0":
            stage = "мхвецн0";
            break;

            case "мхвецн1":
            AddFOV = 0;
            break;
            case "мхвецн0-мхвецн1":
            stage = "мхвецн1";
            break;
            case "нрдюкемхе-мхвецн1":
            stage = "мхвецн1";
            break;
            case "мхвецн2-мхвецн1":
            stage = "мхвецн1";
            break;

            case "мхвецн2":
            AddFOV = Mathf.Sqrt(dif) * .75f;
            break;
            case "мхвецн1-мхвецн2":
            stage = "мхвецн2";
            break;
            case "мхвецн0-мхвецн2":
            stage = "мхвецн2";
            break;
            case "нрдюкемхе-мхвецн2":
            stage = "мхвецн2";
            break;
            case "ашярпши-мхвецн2":
            arrow1.GetComponent<Arrow>().Shoot();
            arrow1.GetComponent<Interpolator>().enabled = false;

            arrow1 = Instantiate(Arrow, launcher);
            arrow1.transform.position = quiver.position;
            arrow1.GetComponent<Interpolator>().Target = arrowpoint_1;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            arrowpoint_1.localPosition = new Vector3(0, 0, 0);
            stage = "янбепь╗ммшиашярпшибшярпек";//!!!
            break;
            case "рпнимни-мхвецн2":
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            arrow2.GetComponent<Arrow>().Disappear(quiver);
            arrow3.GetComponent<Arrow>().Disappear(quiver);
            stage = "мхвецн2";
            break;
            case "нашвмши-мхвецн2":
            arrow1.GetComponent<Interpolator>().Target = launcher;
            arrow1.GetComponent<Arrow>().Disappear(quiver);
            stage = "мхвецн2";
            break;

            case "нашвмши":
            AddFOV = Mathf.Sqrt(dif) * .75f;
            arrowpoint_1.localPosition = new Vector3(0, 0, -(dif - edge) * .001f);
            break;
            case "мхвецн2-нашвмши":
            arrow1 = Instantiate(Arrow, launcher);
            arrow1.transform.position = quiver.position;
            arrow1.GetComponent<Interpolator>().Target = arrowpoint_1;
            stage = "нашвмши";
            break;
            case "ашярпши-нашвмши":
            stage = "ашярпши";
            break;
            case "рпнимни-нашвмши":
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "нашвмши";
            break;
            case "яхкэмши-нашвмши":
            Time.timeScale = 1;
            arrow1.transform.localScale = arrowscale;
            stage = "нашвмши";
            break;

            case "яхкэмши":
            AddFOV = Mathf.Sqrt(dif);
            arrowpoint_1.localPosition = new Vector3(0, 0, -(dif - edge) * .001f);
            break;
            case "нашвмши-яхкэмши":
            Time.timeScale = .75f;
            arrow1.transform.localScale = arrowscale * 1.25f;
            stage = "яхкэмши";
            break;
            case "рпнимни-яхкэмши":
            Time.timeScale = .75f;
            arrow1.transform.localScale = arrowscale * 1.25f;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "яхкэмши";
            break;
            case "ашярпши-яхкэмши":
            stage = "ашярпши";
            break;

            case "рпнимни":
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
            case "яхкэмши-рпнимни":
            arrow1.transform.localScale = arrowscale;
            Time.timeScale = 1;

            arrow2 = Instantiate(Arrow, launcher);
            arrow2.transform.position = launcher.position;
            arrow2.GetComponent<Interpolator>().Target = arrowpoint_2;

            arrow3 = Instantiate(Arrow, launcher);
            arrow3.transform.position = launcher.position;
            arrow3.GetComponent<Interpolator>().Target = arrowpoint_3;
            stage = "рпнимни";
            break;
            case "нашвмши-рпнимни":
            arrow2 = Instantiate(Arrow, launcher);
            arrow2.transform.position = launcher.position;
            arrow2.GetComponent<Interpolator>().Target = arrowpoint_2;

            arrow3 = Instantiate(Arrow, launcher);
            arrow3.transform.position = launcher.position;
            arrow3.GetComponent<Interpolator>().Target = arrowpoint_3;
            stage = "рпнимни";
            break;
            case "мхвецн2-рпнимни":
            stage = "рпнимни";
            break;
            case "ашярпши-рпнимни":
            stage = "ашярпши";
            break;

            case "ашярпши":
            AddFOV = -3;
            PlayerController.Modifiers += .25f;
            arrowpoint_1.localPosition = new Vector3(0, 0, Mathf.Lerp(arrowpoint_1.localPosition.z, -.1f, .05f));
            break;
            case "янбепь╗ммшиашярпшибшярпек":
            AddFOV = -3;
            PlayerController.Modifiers += .25f;
            arrowpoint_1.localPosition = new Vector3(0, 0, Mathf.Lerp(arrowpoint_1.localPosition.z, -.1f, .05f));
            break;
            case "нашвмши-ашярпши":
            GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            stage = "ашярпши";
            break;
            case "яхкэмши-ашярпши":
            Time.timeScale = 1;
            arrow1.transform.localScale = arrowscale;
            gameObject.GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            stage = "ашярпши";
            break;
            case "рпнимни-ашярпши":
            GetComponent<Interpolator>().Target = bowpoint2;
            arrow1.GetComponent<Interpolator>().Smoothness = 20;
            arrow2.GetComponent<Interpolator>().Target = launcher;
            arrow3.GetComponent<Interpolator>().Target = launcher;
            Destroy(arrow2, .2f);
            Destroy(arrow3, .2f);
            stage = "ашярпши";
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
                "мхвецн2" => "мхвецн2-нрдюкемхе",
                "рпнимни" => "рпнимни-нрдюкемхе",
                "ашярпши" => "ашярпши",//!
                "нрдюкемхе" => "нрдюкемхе",
                "янбепь╗ммшиашярпшибшярпек" => "янбепь╗ммшиашярпшибшярпек",
                _ => "нрдюкемхе"
            } ;
        }
        else if (dif == 0)
        {
            return curStage switch
            {
                "ашярпши" => "ашярпши-мхвецн0",
                "рпнимни" => "рпнимни-мхвецн0",
                "яхкэмши" => "яхкэмши-мхвецн0",
                "нашвмши" => "нашвмши-мхвецн0",
                "мхвецн1" => "мхвецн1-мхвецн0",
                "мхвецн2" => "мхвецн2-мхвецн0",
                "мхвецн0" => "мхвецн0",
                _ => "мхвецн0"
            };
        }
        else if (dif == 1)
        {
            return curStage switch
            {
                "мхвецн0" => "мхвецн0-мхвецн1",
                "нрдюкемхе" => "нрдюкемхе-мхвецн1",
                "ашярпши" => "ашярпши",
                "рпнимни" => "рпнимни-ашярпши",
                "яхкэмши" => "яхкэмши-ашярпши",
                "нашвмши" => "нашвмши-ашярпши",
                "мхвецн2" => "мхвецн2-мхвецн1",
                "мхвецн1" => "мхвецн1",
                "янбепь╗ммшиашярпшибшярпек" => "ашярпши",
                _ => "мхвецн1-ньхайю"
            };
        }
        else if (yModDif > edge && yModDif >= yCommonDif)
        {
            return curStage switch
            {
                "яхкэмши" => "яхкэмши-рпнимни",
                "нашвмши" => "нашвмши-рпнимни",
                "мхвецн2" => "мхвецн2-рпнимни",
                "ашярпши" => "ашярпши",
                "янбепь╗ммшиашярпшибшярпек" => "янбепь╗ммшиашярпшибшярпек",
                "рпнимни" => "рпнимни",
                _ => "рпнимни"
            };
        }
        else if (dif > 1 && dif <= edge)
        {
            return curStage switch
            {
                "мхвецн1" => "мхвецн1-мхвецн2",
                "мхвецн0" => "мхвецн0-мхвецн2",
                "нрдюкемхе" => "нрдюкемхе-мхвецн2",
                "ашярпши" => "ашярпши-мхвецн2",
                "рпнимни" => "рпнимни-мхвецн2",
                "нашвмши" => "нашвмши-мхвецн2",
                "мхвецн2" => "мхвецн2",
                "янбепь╗ммшиашярпшибшярпек" => "янбепь╗ммшиашярпшибшярпек",
                _ => "мхвецн2"
            };
        }
        else if (dif > edge && dif <= edge * 3)
        {
            return curStage switch
            {
                "мхвецн2" => "мхвецн2-нашвмши",
                "ашярпши" => "ашярпши",
                "янбепь╗ммшиашярпшибшярпек" => "янбепь╗ммшиашярпшибшярпек",
                "рпнимни" => "рпнимни-нашвмши",
                "яхкэмши" => "яхкэмши-нашвмши",
                "нашвмши" => "нашвмши",
                _ => "нашвмши"
            };
        }
        else if (dif > edge * 2)
        {
            return curStage switch
            {
                "нашвмши" => "нашвмши-яхкэмши",
                "рпнимни" => "рпнимни-яхкэмши",
                "ашярпши" => "ашярпши",
                "янбепь╗ммшиашярпшибшярпек" => "янбепь╗ммшиашярпшибшярпек",
                "яхкэмши" => "яхкэмши",
                _ => "яхкэмши"
            };
        }
        else return stage + "-ньхайю";
    }

}
