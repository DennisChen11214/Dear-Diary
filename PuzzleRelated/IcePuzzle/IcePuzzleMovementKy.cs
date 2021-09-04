using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcePuzzleMovementKy : PuzzleMovement
{
    #region Public Variables
    #endregion
    #region Serialized Fields
    [SerializeField] bool personBehind; //True if this character is in the back
    [SerializeField] Power ability; //Disabled if this character is in the back
    [SerializeField] GameObject other;
    #endregion

    #region Private Variables
    int numSteps;
    static List<Vector3> pastPositions = new List<Vector3>(); //List of positions for the person in the back to use to move
    static Vector3[] swapPositions = new Vector3[2]; //Position of the player in the front and in the back
    static float pastDirection = 1; // For the person in behind to have a proper animation
    float tempPastDirection = 1;
    bool faceAndMove = false;
    bool inItem = false;
    static bool onButton;
    bool hitPerson;
    GameObject itemDetect;
    #endregion

    protected override void Awake() {
        base.Awake();
        controls.Gameplay.SwapChar.performed += ctx => SwapChar();
        controls.Gameplay.OffButton.performed += ctx => SwapControlBack();
        controls.Gameplay.PickUp.performed += ctx => PickUpKey();
        KyPowerIce.onButtonPressed += SwapControl;
        SimonPower.onSwitchFlipped += FlipSwitch;
        //Initialize the swapPositions and pastPositions with the initial positions of the characters
        if(personBehind){
            swapPositions[1] = transform.position;
            ability.enabled = false;
        }
        else{
            pastPositions.Clear();
            swapPositions[0] = transform.position;
            pastPositions.Add(transform.position);
        }
    }

    protected override void Start ()
    {
        base.Start();
        //Change the direction of the person in the back relative to the person in the front
        if(personBehind){
            if(swapPositions[0].x - swapPositions[1].x > 1){
                tempPastDirection = 1;
                pastDirection = 1;
            }
            else if(swapPositions[0].x - swapPositions[1].x < -1){
                tempPastDirection = -1;
                pastDirection = -1;
            }
            else if(swapPositions[0].y - swapPositions[1].y > 1){
                tempPastDirection = 2;
                pastDirection = 2;
            }
            else{
                tempPastDirection = -2;
                pastDirection = -2;
            }
        }
    }

    void Update()
    {
        //Don't have any movement animations in dialogue, scene changes, or when viewing diary pages
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE || GameManager.Instance.fadingIn ||
           GameManager.Instance.CurrentGameState == GameManager.GameState.INVENTORY){
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
            return;
        }
        if(directionDetect != Vector2.zero && !moving){
            CalcDirection();
        }
        //First time hitting a direction makes the character face that way but not move
        else if(!moving)
            faceAndMove = true;
        //Makes it so that the character doesn't play the wrong direction's animation while moving
        if(moving && !animSameDir){
            animSameDir = true;
            animator.SetBool("idle", false);
            //Depending on the direction the character is moving, set the animation variables accordingly
            if((direction == 1 && !personBehind) || (pastDirection == 1 && personBehind)){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if((direction == -1 && !personBehind) || (pastDirection == -1 && personBehind)){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if((direction == 2 && !personBehind) || (pastDirection == 2 && personBehind)){
                animator.SetFloat("yMovement", 1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
            else if((direction == -2 && !personBehind) || (pastDirection == -2 && personBehind)){
                animator.SetFloat("yMovement", -1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
        }
        RaycastHit2D hit2D = new RaycastHit2D();
        if(!moving){
            if(direction != 0){
                tempPastDirection = direction;
            }
            animSameDir = false;
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
            numSteps = 1;
            //Check where the character would end up if they slid across the ice in one direction
            while(!hit2D && directionMoving != Vector2.zero){
                if(personBehind)
                    hit2D = Physics2D.Raycast(swapPositions[0], directionMoving, stepDistance * numSteps, 1 << 9);
                else
                    hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance * numSteps, 1 << 9);
                numSteps++;
                if(hit2D){
                    string tag = hit2D.collider.gameObject.tag;
                    //If we hit a special tile, hole, or exit, move one more tile so we land on it
                    if(tag == "SpecialTile" || tag == "Hole" || tag == "Exit"){
                        numSteps++;
                    }
                    if(tag == "Player")
                        hitPerson = true;
                }
            }
            numSteps -= 2;
        }
        //If the character moves in the same direction they're facing
        if(sameDir && !moving && faceAndMove && numSteps > 0){
            moving = true;
            if(!personBehind){
                newPos = transform.position + (Vector3)directionMoving * stepDistance * numSteps;
            }
            else{
                newPos = swapPositions[0] + (Vector3)directionMoving * stepDistance * (numSteps - 1);
            }
            StartCoroutine(Move(oldPos, newPos));
        }
        else if(sameDir && hit2D){
            sameDir = false;
        }
        if(directionDetect == Vector2.zero){ 
            sameDir = false;
        }
    }

    //Figure out the direction that the character is facing based on input
    protected void CalcDirection(){
        horizontal = directionDetect.x;
        vertical = directionDetect.y;
        //Gets the old direction that the player is facing
        float oldDir = direction;
        //Set the new direction the player is facing
        if(direction == 1 || direction == -1){
            if(horizontal == 0 && vertical != 0)
                direction = 2 * vertical;
            else if(horizontal != 0)
                direction = horizontal;
            else
                direction = 0;
        }
        else{
            if(vertical == 0 && horizontal != 0)
                direction = horizontal;
            else if(vertical != 0)
                direction = 2 * vertical;
            else
                direction = 0;
        }
        //If the player is not idle
        if(direction != 0){
            DirectionFacing = direction;
             //If there's something in the way, still have the character in the front face that way and remain idle
            if(!moving && !personBehind){
                animator.SetBool("idle", true);
                if(direction == 1 && !personBehind){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if(direction == -1 && !personBehind){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(direction == 2 && !personBehind){
                    animator.Play("IdleBack");
                }
                else if(direction == -2 && !personBehind){
                    animator.Play("IdleFront");
                }
            }
        }
        //If we move in the same direction we're facing
        if(oldDir == DirectionFacing && faceAndMove){
            sameDir = true;
        }
        //If we try moving in a different direction we're facing
        else if(oldDir != DirectionFacing){
            faceAndMove = false;
        }
        if(!sameDir){
            sameDir = oldDir == direction && oldDir != 0;
            if(DirectionFacing == 1 || DirectionFacing == -1)
                directionMoving = new Vector2(horizontal, 0);
            else
                directionMoving = new Vector2(0, vertical);
        }
    }

    //If we're not moving, the person in the front, and on an item, pick the key up
    void PickUpKey(){
        if(inItem && !moving && !personBehind){
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.keyItem);
            Item pickedUp = itemDetect.gameObject.GetComponent<Item>();
            if(!HasKey(PlayerInventory.GetInventory()))
                PlayerInventory.AddItem(pickedUp.itemInformation);
            UIManager.Instance.SetEquippedSprite(pickedUp.itemInformation);
            inItem = false;
            itemDetect.SetActive(false);
        }
    }

    //Move a set distance based on the direction facing
    IEnumerator Move(Vector3 oldP, Vector3 newP)
    {
        float timeElapsed = 0;
        //Move the character stepDistance away
        if(!personBehind)
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.iceMove);
        while (timeElapsed < 1)
        {
            if(personBehind){
                //First move to where the person in the front was
                if(timeElapsed < (1.0f / numSteps)){
                    transform.position = Vector3.Lerp(oldPos, pastPositions[0], timeElapsed * numSteps);
                }
                //Then keep moving to the tile behind the person in the front by the end
                else{
                    transform.position = Vector3.Lerp(pastPositions[0], newPos, (timeElapsed - (1.0f / numSteps)) * (numSteps / (numSteps - 1.0f)));
                    RaycastHit2D hit2D = new RaycastHit2D();
                    int steps = 1;
                    //Need to continuously calculate if something's in the way due to boxes being able to disappear/reappear
                    while(!hit2D){
                        hit2D = Physics2D.Raycast(pastPositions[0], directionMoving, stepDistance * steps, 1 << 9);
                        steps++;
                        if(hit2D){
                            string tag = hit2D.collider.gameObject.tag;
                            if(tag == "SpecialTile" || tag == "Hole" || tag == "Exit"){
                                steps++;
                            }
                            if(tag == "Player")
                                hitPerson = true;
                        }
                    }
                    steps -= 2;
                    newP = pastPositions[0] + (Vector3)directionMoving * stepDistance * (steps - 1);
                    //Change the anim direction to face the correct way
                    if(direction == 1){
                        animator.SetFloat("yMovement", 0);
                        animator.SetBool("yMoving", false);
                        animator.SetBool("xMovement", true);
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else if(direction == -1){
                        animator.SetFloat("yMovement", 0);
                        animator.SetBool("yMoving", false);
                        animator.SetBool("xMovement", true);
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if(direction == 2){
                        animator.SetFloat("yMovement", 1);
                        animator.SetBool("yMoving", true);
                        animator.SetBool("xMovement", false);
                    }
                    else if(direction == -2){
                        animator.SetFloat("yMovement", -1);
                        animator.SetBool("yMoving", true);
                        animator.SetBool("xMovement", false);
                    }
                }
            }
            else{
                transform.position = Vector3.Lerp(oldPos, newPos, timeElapsed);
                RaycastHit2D hit2D = new RaycastHit2D();
                int steps = 1;
                //Need to continuously calculate if something's in the way due to boxes being able to disappear/reappear
                while(!hit2D){
                    hit2D = Physics2D.Raycast(oldPos, directionMoving, stepDistance * steps, 1 << 9);
                    steps++;
                    if(hit2D){
                        string tag = hit2D.collider.gameObject.tag;
                        if(tag == "SpecialTile" || tag == "Hole" || tag == "Exit"){
                            steps++;
                        }
                        if(tag == "Player")
                            hitPerson = true;
                    }
                }
                steps -= 2;
                newP = oldPos + (Vector3)directionMoving * stepDistance * steps;
            }
            //Snap to the final position
            if(directionMoving.x > 0 && !newP.Equals(newPos)){
                if(transform.position.x > newP.x){
                    transform.position = newP;
                    break;
                }
            }
            else if(directionMoving.x < 0 && !newP.Equals(newPos)){
                if(transform.position.x < newP.x){
                    transform.position = newP;
                    break;
                }
            }
            else if(directionMoving.y > 0 && !newP.Equals(newPos)){
                if(transform.position.y > newP.y){
                    transform.position = newP;
                    break;
                }
            }
            else{
                if(transform.position.y < newP.y && !newP.Equals(newPos)){
                    transform.position = newP;
                    break;
                }
            }
            timeElapsed += Time.deltaTime / numSteps * 10;
            yield return null;
        }
        if(personBehind || onButton && pastPositions.Count > 0)
            pastPositions.RemoveAt(0);
        transform.position = Vector3.Lerp(oldPos, newP, 1);
        sameDir = false;
        oldPos = transform.position;
        moving = false;
        AudioManager.Instance.Stop();
        if(personBehind){
            swapPositions[1] = transform.position;
        }
        else{
            swapPositions[0] = transform.position;
            pastPositions.Add(transform.position);
            pastDirection = tempPastDirection;
        }
    }

    //Flip the switch in front of simon
    void FlipSwitch(){
        if(ability.charName != Power.Person.SIMON){
            return;
        }
        //Check if there's a switch in front of Simon
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance, 1 << 9);
        if(hit2D && hit2D.collider.gameObject.tag == "Switch" && !moving){
            if(hit2D.collider.gameObject.GetComponent<Switch>().on)
                return;
            hit2D.collider.gameObject.GetComponent<Switch>().on = true;
            hit2D.collider.gameObject.GetComponent<Animator>().Play("Switch");
            //Certain tiles disappear after the switch is flipped
            List<GameObject> tiles = hit2D.collider.gameObject.GetComponent<Switch>().GetSwitchTiles();
            StartCoroutine(ChangeAlpha(tiles));
        }
    }

    //Makes the tiles that are about to disappear fade out
    IEnumerator ChangeAlpha(List<GameObject> tiles){
        float aTime = .25f;
        Color tileColor = tiles[0].GetComponent<SpriteRenderer>().color;
        for (float t = 0; t < 1.0f; t += Time.deltaTime / aTime)
        {
            foreach(GameObject tile in tiles){
                if(tile.activeSelf)
                    tile.GetComponent<SpriteRenderer>().color = new Color(tileColor.r,tileColor.g,tileColor.b, Mathf.Lerp(1,0,t));
            }
            yield return null;
        }
        foreach(GameObject tile in tiles){
            Destroy(tile);
        }
    }

    //Swap the position of both characters and disable the ability of the one in the back
    void SwapChar(){
        if(moving || onButton || GameManager.Instance.CurrentGameState != GameManager.GameState.PUZZLE  || !other.activeSelf)
            return;
        //Both characters swap positions and toggle their abilities
        if(personBehind){
            transform.position = swapPositions[0];
            oldPos = transform.position;
            personBehind = false;
            ability.enabled = true;
        }
        else{
            transform.position = swapPositions[1];
            oldPos = transform.position;
            personBehind = true;
            ability.enabled = false;
        }
        //Changes the direction that the characters are facing based on the direction of the characters before the swap
        if(other.GetComponent<PuzzleMovement>().DirectionFacing == 1){
            animator.Play("IdleSide");
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == -1){
            animator.Play("IdleSide");
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == 2){
            animator.Play("IdleBack");
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == -2){
            animator.Play("IdleFront");
        }
    }

    //Control of movement swaps over to the person not on the button, the person on the button can't move anymore
    void SwapControl(){
        onButton = true;
        if(personBehind){
            oldPos = transform.position;
            personBehind = false;
            ability.enabled = true;
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.leaveKy);
        }
        else{
            //Person on the button is now treated as part of the environment
            gameObject.layer = 9;
            oldPos = transform.position;
            personBehind = true;
            ability.enabled = false;
        }
    }

    //Gets control back of the person on the button
    void SwapControlBack(){
        if(!onButton || !hitPerson){
            return;
        }
        hitPerson = false;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance, 1 << 9);
        //Checks if we're next to the player in the direction we're facing
        if(hit2D && !moving && hit2D.collider.tag == "Player"){
            //Set everything about the character back to normal
            IcePuzzleMovementKy kyMovement = hit2D.collider.gameObject.GetComponent<IcePuzzleMovementKy>();
            kyMovement.enabled = true;
            onButton = false;
            oldPos = transform.position;
            personBehind = true;
            ability.enabled = false;
            kyMovement.gameObject.layer = 0;
            kyMovement.oldPos = kyMovement.gameObject.transform.position;
            kyMovement.personBehind = false;
            kyMovement.ability.enabled = true;
            pastPositions.RemoveAt(0);
            pastPositions.Add(kyMovement.gameObject.transform.position);
            swapPositions[0] = kyMovement.gameObject.transform.position;
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.leaveKy);
        }
    }

    //Game over if we fall in a hole
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Hole"){
            GameManager.Instance.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //If the player made it to the exit with the key
        if(other.gameObject.tag == "Exit" && HasKey(PlayerInventory.GetInventory()) && !onButton){
            RemoveKey(PlayerInventory.GetInventory());
            //Start a conversation at the end of the puzzle and add a page to the diary
            if(GameManager.Instance.day == 2){
                DialogueManager.Instance.StartConversation("Day2PuzzleEndSimon");
                ItemInformation diaryEndPage = new ItemInformation();
                diaryEndPage.itemType = ItemInformation.ItemType.Diary;
                diaryEndPage.day = 2;
                diaryEndPage.end = true;
                Diary.AddPage(diaryEndPage);
            }
            //Day 4 - start dialogues based on if Nikko/Simon was with us on previous days
            else if(GameManager.Instance.day == 4){ 
                if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle2NeutralSimonEnd");
                else if(DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle2KyEnd");
                else{
                    DialogueManager.Instance.puzzleDialogueDone = false;
                    GameManager.Instance.LoadScene("Day4ThirdPuzzle", true, true);
                }
            }
        }    
        //Indicate that we're on an item
        if(other.gameObject.tag.Equals("Item")){
           inItem = true;
           itemDetect = other.gameObject;
        }
    }
    private void OnDestroy() {
        KyPowerIce.onButtonPressed -= SwapControl;
        SimonPower.onSwitchFlipped -= FlipSwitch;
        onButton = false;
    }
}
