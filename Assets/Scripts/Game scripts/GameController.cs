using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] figuresOrder;

    public void StartGame()
    {
        Debug.Log("game was started");

        figuresOrder = GameObject.FindGameObjectsWithTag("EnemyFigure").Concat(GameObject.FindGameObjectsWithTag("PlayerFigure")).ToArray();
        GameObject.FindGameObjectsWithTag("StartButton")[0].GetComponentInChildren<Text>().text = "Pause Game";


        StartCoroutine(Steps());

        // zacnem prechadzat polom figures order
        // v poli figures order su figurky v poradi v akom pojdu
        // ked prejdem cele pole idem od zaciatku
        // figurky ktore su uz vyhodene preskakujem
        // ak figurka nema uz dalsi tah ona si to handluje
        // ja vzdy len zavolam figure.step() a ona sa pohne

        // hned ako sa zavola start game pustim metodu make figuress order, tato
        // metoda prejde vsetkymi figurkami
        // figurka ma v sebe atribut ktory urcuje jej celkove poradie
        // hracove figurky budu mat neparne cisla

        // protihracove inicializujem zo sceny s parnymi cislami

        // kazda figurka bude mat order script ktory bude handlovat vsetko ohladne poradia
        // order script bude komunikovat s UI textom pri figurke a bude zobrazovat poradie

        // velkost policka je 1,95x1,95 cize ak sa figurka pohne viem lahko vypocitat pomocou suradnic
        // nasledne spravim ray dole a prve co tam bude musi byt policko
    }


    private IEnumerator Steps()
    {
        while (true)
        {
            foreach (GameObject figure in figuresOrder)
            {
                figure.GetComponent<PawnScript>().Move();
                yield return new WaitForSeconds(1);
            }
        }
    }
}
