using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

    public enum BlockBehaviours
    {
        Level1,
        Level2,
        Level3
    }

public class Block : MonoBehaviour
{
    public static Action<Block> OnBlockDestroyed;
    public List<Sprite> LifeSprites = new List<Sprite>();
    public Color[] BlockColor;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private int _blockLifes = 3;
    private BlockBehaviours _currentLevel;
    private Vector3 _startPosition; 
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        //_currentLevel = GetCurrenLevel();
        _startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("ball"))
        {
            LevelBehaviorManager.Instance.GetBehaviour()(this);
        }
    }
    

    private void Update()
    {
       transform.Rotate(new Vector3(0, 0, 5 * Time.deltaTime));
        float destroyBorderPositionY = -6f;
        if(transform.position.y < destroyBorderPositionY)
        {
            OnBlockDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void GridBlocksBehaviour()
    {
        Debug.Log("поведение 1");
        _blockLifes--;
        _renderer.sprite = LifeSprites[_blockLifes];
        if(_blockLifes == 0)
        {
            _collider.enabled = false;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        _renderer.color = BlockColor[_blockLifes];
    }    

    public void SpiralBlocksBehaviour()
    {
        float radius = 0.5f;
        int randomBlockLifes = Random.Range(1, 5);
        randomBlockLifes--;
        if( randomBlockLifes == 0)
        {
            _collider.enabled = false;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        Vector3 offset = Random.insideUnitCircle * radius;
        transform.position = new Vector3(_startPosition.x + offset.x, _startPosition.y + offset.y, 0);
        _renderer.color = new Color(Random.value, Random.value, Random.value);
    }

    public void CircleBlocksBehaviour()
    {
        Vector3 intialPosition = transform.position;

        float moveDistance = 0.5f;
        float destroyBorderPositionY = -8f;
        float startX = intialPosition.x;
        float targetLeftX = startX - moveDistance;
        float targetRightX = startX + moveDistance;

        StartCoroutine(DelBlockAndSrink());

        IEnumerator DelBlockAndSrink()
        {
            Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);
            float elapsedTime = 0f;
            float shrinkTime = 5f;
            float shrinkSpeed = 5f;

            while(elapsedTime < shrinkTime)
            {

            while(transform.position.x > targetLeftX)
            {
                float newX = Mathf.MoveTowards(transform.position.x, targetLeftX, 2 * Time.deltaTime);
                float newY = Mathf.MoveTowards(transform.position.y, destroyBorderPositionY, 2 * Time.deltaTime);
                transform.position = new Vector3(newX, newY, transform.position.z);
                yield return null;
            }

            while(transform.position.x < targetRightX)
            {
                float newX = Mathf.MoveTowards(transform.position.x, targetRightX, 2 * Time.deltaTime);                
                float newY = Mathf.MoveTowards(transform.position.y, destroyBorderPositionY, 2 * Time.deltaTime);
                transform.position = new Vector3(newX, newY, transform.position.z);
                yield return null;
            }

                elapsedTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, shrinkSpeed * Time.deltaTime);
                yield return null;
            }

            Destroy(gameObject);            
        }

        
    }       
}
