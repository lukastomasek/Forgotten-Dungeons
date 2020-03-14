using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukasScripts.AI;
//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public abstract class PlayerBaseState
	{
        public abstract void EnterState(EnemyController_FSM enemy);

        public abstract void Update(EnemyController_FSM enemy);

        public abstract void OnCollisionEnter(EnemyController_FSM enemy);
	}

}