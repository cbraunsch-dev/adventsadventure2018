﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The mechanic works like this: A particle orbits a sphere and passes through a visor. If the player hits the space bar while the particle is inside the
// visor, the sphere's radius doubles and the particle's speed increases. If the user manages to hit the space bar again while the particle is inside the
// visor, the sphere's radius is doubled one again. If the player manages to hit the space bar at the right time again, the sphere's radius goes back to
// initial value. The speed of the particle stays the same. So there are 3 levels before the whole setup shrinks back to its initial size. Once it's at
// its initial size, the whole spiel starts over again: It increases in size twice before shrinking down to its initial size again. For the sake of making the
// documentation a tad clearer, the different sizes of the sphere and positions of the visor and particle are referred to as levels. Level 1 being the initial
// position and size of the sphere, particle and visor, level 2 being the position and size when the user times the space bar correctly once, level 3 when
// the user times it again a second time. After level 3, it goes back to level 1 but the speed of the particle does not change.
public class MechanicManagerBehavior : MonoBehaviour
{
    private bool insideVisor = false;
    private const float maxRadius = 2.5f;
    private const int initialRadius = 1;
    private int score = 1;

    public GameObject visor;
    public GameObject particle;
    public GameObject sphere;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && insideVisor)
        {
            Debug.Log("Nice! Next level!");
            score++;
            this.insideVisor = false;
            if(particle.GetComponent<ParticleBehavior>().radius < maxRadius) {
                this.Grow();
            } else {
                this.ShrinkToInitialSize();
            }
        }
        else if (Input.GetKeyDown("space"))
        {
            Debug.Log("Sorry! Try again!");
            var gameManagerBehavior = GameObject.FindWithTag(Tags.GameManager).GetComponent<GameManagerBehavior>();
            gameManagerBehavior.PlayerEarnedMovementScore(this.score);
        }
    }

    private void Grow()
    {
        //Scale center
        sphere.transform.localScale = sphere.transform.localScale * 2;

        //Set position and speed of particle
        var currentRadius = particle.GetComponent<ParticleBehavior>().radius;
        var increaseInRadius = currentRadius - 0.5;
        var newRadius = currentRadius + increaseInRadius;
        particle.GetComponent<ParticleBehavior>().radius = (float)newRadius; //Goes from: 1 to 1.5 then to 2.5 then to 4.5 (increase by 0.5, by 1, by 2 etc)
        particle.GetComponent<ParticleBehavior>().rotationSpeed *= 2;

        //Set position of visor 
        visor.transform.position = new Vector3(visor.transform.position.x, visor.transform.position.y * 2, visor.transform.position.z);
    }

    private void ShrinkToInitialSize() {
		//Scale center. We divide they scale by 4 because that will bring it back to level 1.
		sphere.transform.localScale = sphere.transform.localScale / 4;

		//Set position and speed of particle
        particle.GetComponent<ParticleBehavior>().radius = initialRadius;

		//Set position of visor. We divide they position by 4 because the visor will only have increased its y position by 2 twice (once when going
        // from the initial position to level 2 and once when going from level 2 to level 3.
		visor.transform.position = new Vector3(visor.transform.position.x, visor.transform.position.y / 4, visor.transform.position.z);
    }

    public void DidEnterVisor()
    {
        this.insideVisor = true;
    }

    public void DidExitVisor()
    {
        this.insideVisor = false;
    }
}
