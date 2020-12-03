using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class BaseManager :MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject
    impAbox, impBbox,
    impA, impB,
    chapAbox, chapBbox,
    chapA, chapB,
    imp3DAbox, imp3DBbox,
    imp3DA, imp3DB,
        plotAbox, plotBbox,
        plotA, plotB,
        indAbox, indBbox,
        indA, indB,
        merchAbox, merchBbox,
        merchA, merchB,
        sc1Abox, sc1Bbox,
        sc1A, sc1B,
        sc2Abox, sc2Bbox,
        sc2A, sc2B,
        sc3Abox, sc3Bbox,
        sc3A, sc3B;
    [SerializeField] bool
            impAboxb, impBboxb,
    impAb, impBb,
    chapAboxb, chapBboxb,
    chapAb, chapBb,
    imp3DAboxb, imp3DBboxb,
    imp3DAb, imp3DBb,
        plotAboxb, plotBboxb,
        plotAb, plotBb,
        indAboxb, indBboxb,
        indAb, indBb,
        merchAboxb, merchBboxb,
        merchAb, merchBb,
        sc1Aboxb, sc1Bboxb,
        sc1Ab, sc1Bb,
        sc2Aboxb, sc2Bboxb,
        sc2Ab, sc2Bb,
        sc3Aboxb, sc3Bboxb,
        sc3Ab, sc3Bb;
    private void Update()
    {
        
    }
    public void RPCActivate(bool team, int what) 
    {
        photonView.RPC("ActivateItem",RpcTarget.AllViaServer,team,what);
    }

    [PunRPC]
public void ActivateItem(bool team,int whatItem) 
    {
        switch (team) 
        {
            case true:
                switch (whatItem) 
                {
                    //---Impresora
                    case 0:
                        impAboxb = true;
                        impAbox.SetActive(true);
                        break;
                    case 1:
                        impAboxb = false;
                        impAb = true;
                        impAbox.SetActive(false);
                        impA.SetActive(true);
                        break;
                    //---- Chapas
                    case 2:
                        chapAboxb = true;
                        chapAbox.SetActive(true);
                        break;
                    case 3:
                        chapAboxb = false;
                        chapAb = true;
                        chapAbox.SetActive(false);
                        chapA.SetActive(true);
                        break;
                    //----Imp3d
                    case 4:
                        imp3DAboxb = true;
                        imp3DAbox.SetActive(true);
                        break;
                    case 5:
                        imp3DAboxb = false;
                        imp3DAb = true;
                        imp3DAbox.SetActive(false);
                        imp3DA.SetActive(true);
                        break;
                    //--Plotter
                    case 6:
                        plotAboxb = true;
                        plotAbox.SetActive(true);
                        break;
                    case 7:
                        plotAboxb = false;
                        plotAb = true;
                        plotAbox.SetActive(false);
                        plotA.SetActive(true);
                        break;
                    //---Indus
                    case 8:
                        indAboxb = true;
                        indAbox.SetActive(true);
                        break;
                    case 9:
                        indAboxb = false;
                        indAb = true;
                        indAbox.SetActive(false);
                        indA.SetActive(true);
                        break;
                    //---Merch
                    case 10:
                        merchAboxb = true;
                        merchAbox.SetActive(true);
                        break;
                    case 11:
                        merchAboxb = false;
                        merchAb = true;
                        merchAbox.SetActive(false);
                        merchA.SetActive(true);
                        break;
                    //---SC1
                    case 12:
                        sc1Aboxb = true;
                        sc1Abox.SetActive(true);
                        break;
                    case 13:
                        sc1Aboxb = false;
                        sc1Ab = true;
                        sc1Abox.SetActive(false);
                        sc1A.SetActive(true);
                        break;
                    //---SC2
                    case 14:
                        sc2Aboxb = true;
                        sc2Abox.SetActive(true);
                        break;
                    case 15:
                        sc2Aboxb = false;
                        sc2Ab = true;
                        sc2Abox.SetActive(false);
                        sc2A.SetActive(true);
                        break;
                    //---SC3
                    case 16:
                        sc3Aboxb = true;
                        sc3Abox.SetActive(true);
                        break;
                    case 17:
                        sc3Aboxb = false;
                        sc3Ab = true;
                        sc3Abox.SetActive(false);
                        sc3A.SetActive(true);
                        break;
                }
                break;
            case false:
                switch (whatItem)
                {
                    //---Impresora
                    case 18:
                        impBbox.SetActive(true);
                        break;
                    case 19:
                        impBbox.SetActive(false);
                        impB.SetActive(true);
                        break;
                    //---- Chapas
                    case 20:
                        chapBbox.SetActive(true);
                        break;
                    case 21:
                        chapBbox.SetActive(false);
                        chapB.SetActive(true);
                        break;
                    //----Imp3d
                    case 22:
                        imp3DBbox.SetActive(true);
                        break;
                    case 23:
                        imp3DBbox.SetActive(false);
                        imp3DB.SetActive(true);
                        break;
                    //--Plotter
                    case 24:
                        plotBbox.SetActive(true);
                        break;
                    case 25:
                        plotBbox.SetActive(false);
                        plotB.SetActive(true);
                        break;
                    //---Indus
                    case 26:
                        indBbox.SetActive(true);
                        break;
                    case 27:
                        indBbox.SetActive(false);
                        indB.SetActive(true);
                        break;
                    //---Merch
                    case 28:
                        merchBbox.SetActive(true);
                        break;
                    case 29:
                        merchBbox.SetActive(false);
                        merchB.SetActive(true);
                        break;
                    //---SC1
                    case 30:
                        sc1Bbox.SetActive(true);
                        break;
                    case 31:
                        sc1Bbox.SetActive(false);
                        sc1B.SetActive(true);
                        break;
                    //---SC2
                    case 32:
                        sc2Bbox.SetActive(true);
                        break;
                    case 33:
                        sc2Bbox.SetActive(false);
                        sc2B.SetActive(true);
                        break;
                    //---SC3
                    case 34:
                        sc3Bbox.SetActive(true);
                        break;
                    case 35:
                        sc3Bbox.SetActive(false);
                        sc3B.SetActive(true);
                        break;
                }
                break;
        }
    }
}
