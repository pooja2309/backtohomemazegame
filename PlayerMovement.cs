using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public AudioSource tickSource;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    // Start is called before the first frame update
    void Start()
    {
        tickSource=GetComponent<AudioSource>();
        animator = GetComponent <Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX",0);
        animator.SetFloat("moveY",-1);
    }

    //Destroy collectables
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Coins")){
            tickSource.Play();
            Destroy(other.gameObject);
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger){
            StartCoroutine(AttackCo());
        }
        else if (change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        }
    }

    private IEnumerator AttackCo(){
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void MoveCharacter(){
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Knock(float knockTime , float damage){
        currentHealth.RunTimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.RunTimeValue > 0){
            StartCoroutine(KnockCo(knockTime));
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator KnockCo(float knockTime){
        if(myRigidbody != null){
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
