using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : MonoBehaviour
{
    public int wheatherTime=0;

    public Camera myCam;

    public GameObject rain;

    GameObject piano;

    public Material sunsetRed;
    public Material night;
    public Material afternoonSky;

    public Light DirectionLight;

    public GameObject fireLight1;
    public GameObject fireLight2;
    public GameObject fireSound;
    public GameObject fireSmoke;

    public List<Light> floor1Light;

    public GameObject interactGoj;
    void Start()
    {
        rain.SetActive(false);

        RenderSettings.skybox = afternoonSky;
        RenderSettings.fogColor = new Color32(255, 255, 190, 255);
        RenderSettings.ambientIntensity = 2f;

        DirectionLight.color = new Color32(255, 255, 200, 255);

        fireLight1.SetActive(false);
        fireLight2.SetActive(false);
        fireSound.SetActive(false);
        fireSmoke.SetActive(false);

        for (int i = 0; i < 70; i++)
        {
            floor1Light[i].color = new Color32(0, 0, 0, 0);        // ���� OFF
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraCenterRay();
    }

    void CameraCenterRay()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))        // ���콺 Ŭ����
        {
            var ray = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            print(hit.transform.gameObject.name);       // �̸��� ȣ��

            // �ǾƳ� Ŭ����
            if (hit.transform.gameObject.name == "Piano_low" )       
            {
                wheatherTime++;
                print(wheatherTime % 4);

                if (wheatherTime % 4==1)        // ���� ������
                {
                    rain.SetActive(false);

                    RenderSettings.skybox = sunsetRed;
                    RenderSettings.fogColor = new Color32(229, 90, 1, 255);
                    RenderSettings.ambientIntensity = 0.05f;

                    DirectionLight.color = new Color32(106, 58, 27, 255);
                    fireLight1.SetActive(true);
                    fireLight2.SetActive(true);
                    fireSound.SetActive(true);
                    fireSmoke.SetActive(true);

                    for (int i = 0; i < 70; i++)
                    {
                        floor1Light[i].color = new Color32(229, 90, 1, 255);        
                    }
                }

                else if(wheatherTime % 4 == 2)      // ���ϴ� �޺�
                {
                    rain.SetActive(false);

                    RenderSettings.skybox = night;
                    RenderSettings.fogColor = new Color32(166, 166, 166, 150);
                    RenderSettings.ambientIntensity = 0.02f;

                    DirectionLight.color = new Color32(80, 80, 80, 255);
                    fireLight1.SetActive(true);
                    fireLight2.SetActive(true);
                    fireSound.SetActive(true);
                    fireSmoke.SetActive(true);

                    for (int i = 0; i < 70; i++)
                    {
                        floor1Light[i].color = new Color32(180, 180, 180, 150);     
                    }
                }

                else if (wheatherTime % 4 == 3)     // ����� ���ϴ� �޺�
                {
                    rain.SetActive(true);

                    RenderSettings.skybox = night;
                    RenderSettings.fogColor = new Color32(166, 166, 166, 150);
                    RenderSettings.ambientIntensity = 0.02f;

                    DirectionLight.color = new Color32(80, 80, 80, 255);

                    fireLight1.SetActive(false);
                    fireLight2.SetActive(false);
                    fireSound.SetActive(false);
                    fireSmoke.SetActive(true);
                }

                else if (wheatherTime % 4 == 0)     // ���� �ϴ� �߰ſ� �¾�
                {
                    rain.SetActive(false);

                    RenderSettings.skybox = afternoonSky;
                    RenderSettings.fogColor = new Color32(255, 255, 190, 200);
                    RenderSettings.ambientIntensity = 2f;

                    DirectionLight.color = new Color32(255, 255, 200, 200);

                    fireLight1.SetActive(false);
                    fireLight2.SetActive(false);
                    fireSound.SetActive(false);
                    fireSmoke.SetActive(false);

                    for (int i = 0; i < 70; i++)
                    {
                        floor1Light[i].color = new Color32(0, 0, 0, 0);        // ���� OFF
                    }
                }
            }

            //���� â�� Ŭ����
            else if(hit.transform.gameObject.name == "BackFLeft" || hit.transform.gameObject.name == "BackFLeftHandle")
            {
                hit.transform.gameObject.name = "BackFLeft";
                //Transform door = hit.transform;
                hit.transform.gameObject.GetComponent<InteractObJ>().OnConnect();
                print("�Լ��۵�");
            }

            else if (hit.transform.gameObject.name == "TestCube" || hit.transform.gameObject.name == "GameObject123")
            {
                hit.transform.gameObject.GetComponent<InteractObJ>().ChangePosition();
                print("�׽�Ʈ�Լ��۵�");

                
            }

            else if (hit.transform.gameObject.name == "Front1L" || hit.transform.gameObject.name == "Front1R" || hit.transform.gameObject.name == "Laptoptest_low")
            {
                hit.transform.gameObject.GetComponent<InteractObJ>().OnConnect();
                print("�����Լ� �۵�");
            }
        }
        
    }
}
