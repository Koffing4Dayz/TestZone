using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPC_StatePattern : MonoBehaviour
    {
        private float checkRate = 0.1f;
        private float nextCheck;

        public float SightRange = 40;
        public float DetectBehindRange = 5;
        public float MeleeAttackRange = 4;
        public float MeleeAttackDamage = 10;
        public float RangeAttackRange = 35;
        public float RangeAttackDamage = 5;
        public float RangeAttackSpread = 0.5f;
        public float AttackRate = 0.4f;
        public float NextAttack;
        public float FleeRange = 25;
        public float Offset = 0.4f;
        public float RequiredDetectCount = 15;
        public float StruckWaitTime = 1.5f;

        public bool canRangeAttack;
        public bool canMeleeAttack;
        public bool isMeleeAttacking;

        public Transform TargetFollow;
        [HideInInspector]
        public Transform TargetPursue;
        [HideInInspector]
        public Vector3 LocationOfInterest;
        [HideInInspector]
        public Vector3 TargetWander;
        [HideInInspector]
        public Transform Attacker;

        public LayerMask SightLayers;
        public LayerMask EnemyLayers;
        public LayerMask FriendlyLayer;
        public string[] EnemyTags;
        public string[] FriendlyTags;

        public Transform[] Waypoints;
        public Transform Head;
        public MeshRenderer MeshRendererFlag;
        public GameObject RangeWeapon;
        public NPC_Master MasterNPC;
        [HideInInspector]
        public NavMeshAgent myNavMeshAgent;

        public NPCState_Interface CurrentState;
        public NPCState_Interface CapturedState;
        public NPCState_Patrol PatrolState;
        public NPCState_Alert AlertState;
        public NPCState_Pursue PursueState;
        public NPCState_MeleeAttack MeleeAttackState;
        public NPCState_RangeAttack RangeAttackState;
        public NPCState_Flee FleeState;
        public NPCState_Struck StruckState;
        public NPCState_InvestigateHarm InvestigateHarmState;
        public NPCState_Follow FollowState;

        private void Awake()
        {
            SetupStateRefrences();
            Initialize();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterNPC.EventNpcLowHealth += ActivateFleeState;
            MasterNPC.EventNpcHealthRecovered += ActivatePatrolState;
            MasterNPC.EventNpcDeductHealth += ActivteStruckState;
            MasterNPC.UpdateLoop += RunUpdate;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcLowHealth -= ActivateFleeState;
            MasterNPC.EventNpcHealthRecovered -= ActivatePatrolState;
            MasterNPC.EventNpcDeductHealth -= ActivteStruckState;
            MasterNPC.UpdateLoop -= RunUpdate;
            StopAllCoroutines();
        }

        private void RunUpdate()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CurrentState.UpdateState();
            }
        }

        private void SetupStateRefrences()
        {
            PatrolState = new NPCState_Patrol(this);
            AlertState = new NPCState_Alert(this);
            PursueState = new NPCState_Pursue(this);
            FleeState = new NPCState_Flee(this);
        }

        private void Initialize()
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            ActivatePatrolState();
        }

        private void ActivatePatrolState()
        {
            CurrentState = PatrolState;
        }

        private void ActivateFleeState()
        {
            if (CurrentState == StruckState)
            {
                CapturedState = FleeState;
                return;
            }

            CurrentState = FleeState;
        }

        private void ActivteStruckState(int dummy)
        {
            StopAllCoroutines();

            if (CurrentState != StruckState)
            {
                CapturedState = CurrentState;
            }

            if (RangeWeapon != null)
            {
                RangeWeapon.SetActive(false);
            }

            if (myNavMeshAgent.enabled)
            {
                myNavMeshAgent.isStopped = true;
            }

            CurrentState = StruckState;

            MasterNPC.CallEventNpcStruckAnim();

            StartCoroutine(RecoverFromStruckState());
        }

        IEnumerator RecoverFromStruckState()
        {
            yield return new WaitForSeconds(StruckWaitTime);

            MasterNPC.CallEventNpcRecoveredAnim();

            if (RangeWeapon != null)
            {
                RangeWeapon.SetActive(true);
            }

            if (myNavMeshAgent.enabled)
            {
                myNavMeshAgent.isStopped = false;
            }

            CurrentState = CapturedState;
        }

        public void OnEnemyAttack() // Called in attack anim
        {
            if (TargetPursue != null)
            {
                if (Vector3.Distance(transform.position, TargetPursue.position) <= MeleeAttackRange)
                {
                    Vector3 toOther = TargetPursue.position - transform.position;
                    if (Vector3.Dot(toOther, transform.forward) > 0.5f)
                    {
                        TargetPursue.SendMessage("CallEventPlayerHealthDeduction", MeleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                        TargetPursue.SendMessage("ProcessDamage", MeleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            isMeleeAttacking = false;
        }

        public void SetAttacker(Transform attacker)
        {
            Attacker = attacker;
        }

        public void Distract(Vector3 distractPos)
        {
            LocationOfInterest = distractPos;

            if (CurrentState == PatrolState)
            {
                CurrentState = AlertState;
            }
        }
    }
}
