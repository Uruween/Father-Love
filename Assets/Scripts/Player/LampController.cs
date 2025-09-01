using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] float counter;  
    [SerializeField] float life = 100f;  
    [SerializeField] GameObject ligth;  

    private void Start()
    {
        ligth.SetActive(false);  
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && life > 0)  
        {
            ligth.SetActive(true);  
            counter += Time.deltaTime;  

            if (counter > 0.1f)  
            {
                life -= 10f * Time.deltaTime;  

                
                if (life <= 0)
                {
                    life = 0;
                    ligth.SetActive(false);  
                }
            }
        }
        else
        {
            ligth.SetActive(false);  
            counter = 0;  
        }
    }

    public void LifeLamp(float cure)
    {
        life += cure;  
        if (life > 100)
        {
            life = 100;  
        }
    }
}
