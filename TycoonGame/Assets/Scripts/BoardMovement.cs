using UnityEngine;

public class BoardMovement : MonoBehaviour
{
    public MeshRenderer[] renderers = new MeshRenderer[8];
    private bool isMovingToSale = false;
    [SerializeField] private Transform salePos;

    public void SendToSale()
    {
        isMovingToSale = true;
    }
    public void Sale()
    {
        
    }
    void Start()
    {
        for (int i = 1; i < 8; i++)
        {
            renderers[i].enabled = false;
        }
    }
    void Update()
    {
        if (isMovingToSale)
        {
            transform.position = Vector3.MoveTowards(transform.position, salePos.position, SpawnerManager.GlobalSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, salePos.position) < 0.05f)
            {
                Destroy(gameObject);
                MenuController.AddMoney(100);
                
            }
        }
    }
}