using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MyAssets
{
    public class XRGrabNetworkInteractable : XRGrabInteractable
    {
        private PhotonView photonView;

        private Rigidbody _rigidbody;
    
        // Start is called before the first frame update
        void Start()
        {
            photonView = GetComponent<PhotonView>();

            _rigidbody ??= GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            photonView.RequestOwnership();
            photonView.RPC("DisableGravity", RpcTarget.Others);
            
            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
    {
        photonView.RPC("EnableGravity", RpcTarget.Others);
        base.OnSelectExited(args);
    }

        [PunRPC]
        public void DisableGravity()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
        [PunRPC]
        public void EnableGravity()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
        }
    }
}
