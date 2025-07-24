using UnityEngine;
using System;
using System.Collections;

public class WorkStation : MonoBehaviour
{
    // [SerializeField] private ParticleSystem sparks;
    private Vector3 stationOffset = new Vector3(0, 0.1f, 0);
    private Vector3 approachOffset = new Vector3(0, 0.1f, 0.31f);

    public bool IsBusy { get; private set; }

    public event Action<WorkStation> OnWorkFinished;

    public void AssignBoard(GameObject board)
    {
        IsBusy = true;
        StartCoroutine(DoWork(board));
    }

    private IEnumerator DoWork(GameObject board)
    {
        Animation anim = GetComponent<Animation>();

        // 1. Подъезд к подходу
        Vector3 approachPos = transform.position + approachOffset;
        yield return MoveTo(board, approachPos);

        // 2. Смещение к станку (вправо, например)
        Vector3 stationPos = transform.position + stationOffset;
        yield return MoveTo(board, stationPos);

        // 3. Анимация
        // sparks.Play();
        anim.Play("MACHINE_SOLDERING");
        yield return new WaitUntil(() => !anim.isPlaying);
        // sparks.Stop();
        // 4. Смена комплектующих
        board.GetComponent<BoardMovement>().renderers[1].enabled = true;
        board.GetComponent<BoardMovement>().renderers[0].enabled = false;

        // 5. Возврат обратно на подход
        yield return MoveTo(board, approachPos);

        // 6. Едем на продажу
        board.GetComponent<BoardMovement>().SendToSale();

        IsBusy = false;
        OnWorkFinished?.Invoke(this);
    }
    private IEnumerator MoveTo(GameObject board, Vector3 targetPos)
    {
        while (Vector3.Distance(board.transform.position, targetPos) > 0.01f)
        {
            board.transform.position = Vector3.MoveTowards(
                board.transform.position,
                targetPos,
                SpawnerManager.GlobalSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}