using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;   // �� �t�H�[�J�X����ɕK�v


/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private ClearTimeSO clearTimeSO;

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctIndex;

        public Question(string questionText, string[] answers, int correctIndex)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.correctIndex = correctIndex;
        }
    }

    [Header("UI References")]
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text questionNumberText;
    [SerializeField] TMP_Text nextButtonText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] Button[] answerButtons;
    [SerializeField] TMP_Text[] answerTexts;
    [SerializeField] Image correctImage, incorrectImage;
    [SerializeField] GameObject nextQuestionButton;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Image resultImage;

    [Header("Assets")]
    [SerializeField] List<Sprite> resultSprites;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;

    [Header("CSV Settings")]
    [SerializeField] private string csvFileName = "quiz_data"; // Resources/quiz_data.csv

    [Header("Fireworks")]
    [SerializeField] private GameObject fireworkPrefab;
    [SerializeField] private Transform fireworksParent;

    private List<Question> questionList = new List<Question>();
    private Question[] questions;

    private int currentQuestionIndex = 0;
    private int correctCount = 0;
    private int defaultCount = 0;

    void Start()
    {
        LoadQuestionsFromCSV();
        questions = questionList.ToArray();

        Debug.Log($"[Start] �ǂݍ��񂾖�萔: {questions.Length}");

        nextQuestionButton.SetActive(false);
        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        resultPanel.SetActive(false);

        correctCount = defaultCount;
        LoadQuestion();
    }

    private void LoadQuestionsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
        if (csvFile == null)
        {
            Debug.LogError("CSV�t�@�C����������܂���: " + csvFileName);
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        questionList.Clear();

        for (int i = 1; i < lines.Length; i++) // �w�b�_�[���X�L�b�v
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] columns = lines[i].Split(',');
            if (columns.Length < 6) continue;  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B

            string question = columns[0].Trim();
            string[] answers = new string[4];  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            for (int j = 0; j < 4; j++)  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            {
                answers[j] = columns[j + 1].Trim();
            }

            if (!int.TryParse(columns[5].Trim(), out int correctIndex))  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            {
                Debug.LogWarning($"�����C���f�b�N�X�������ł�: {lines[i]}");
                continue;
            }

            if (correctIndex < 0 || correctIndex >= 4)  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            {
                Debug.LogWarning($"�����C���f�b�N�X���͈͊O�ł�: {lines[i]}");
                continue;
            }

            questionList.Add(new Question(question, answers, correctIndex));
        }

        Debug.Log($"[LoadQuestionsFromCSV] ��萔: {questionList.Count}");
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            resultPanel.SetActive(true);
            ShowResultImage(correctCount);

            // �ԉΉ��o
            if (correctCount == questions.Length)
            {
                audioSource.PlayOneShot(audioClips[2]);
                StartCoroutine(PlayFireworksSequence(3));  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
                messageText.text = "�p�[�t�F�N�g�I�I";
            }
            else if (correctCount >= questions.Length - 2)  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
            {
                audioSource.PlayOneShot(audioClips[2]);  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
                StartCoroutine(PlayFireworksSequence(1));
                messageText.text = "�������I���Ƃ�����ƁI";
            }
            else
            {
                audioSource.PlayOneShot(audioClips[3]);
                messageText.text = "�c�O...";
            }

            resultText.text = $"{correctCount}�␳���I";

            // ���ʉ�ʂŃt�H�[�J�X����UI�i��F���g���C�{�^���Ȃǁj
            var firstButton = resultPanel.GetComponentInChildren<Button>();
            if (firstButton != null)
                EventSystem.current.SetSelectedGameObject(firstButton.gameObject);

            Debug.Log($"[���ʕ\��] ����: {correctCount}, ��萔: {questions.Length}");
            return;
        }

        questionNumberText.text = $"Q{currentQuestionIndex + 1}.";
        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerTexts[i].text = q.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
            answerButtons[i].interactable = true;
        }

        correctImage.gameObject.SetActive(false);
        incorrectImage.gameObject.SetActive(false);
        nextQuestionButton.SetActive(false);

        nextButtonText.text = (currentQuestionIndex == questions.Length - 1) ? "���ʔ��\" : "���̖���";

        // �ŏ��̉񓚃{�^����I����Ԃɂ���i�R���g���[���[�p�j
        EventSystem.current.SetSelectedGameObject(answerButtons[0].gameObject);
    }

    void Answer(int index)
    {
        foreach (var btn in answerButtons)
            btn.interactable = false;

        if (index == questions[currentQuestionIndex].correctIndex)
        {
            correctImage.gameObject.SetActive(true);
            correctCount++;
            audioSource.PlayOneShot(audioClips[0]);
            Debug.Log($"[Answer] ����! {currentQuestionIndex}, ����: {correctCount}");
        }
        else
        {
            incorrectImage.gameObject.SetActive(true);
            audioSource.PlayOneShot(audioClips[1]);
            Debug.Log($"[Answer] �s���� {currentQuestionIndex}");
        }

        nextQuestionButton.SetActive(true);

        // �u���̖��ցv�{�^���Ƀt�H�[�J�X���ڂ�
        EventSystem.current.SetSelectedGameObject(nextQuestionButton);
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        LoadQuestion();
    }

    void ShowResultImage(int correctCount)
    {
        int spriteIndex = Mathf.Clamp(correctCount, 0, resultSprites.Count - 1);
        resultImage.sprite = resultSprites[spriteIndex];
        resultImage.gameObject.SetActive(true);
    }

    private IEnumerator PlayFireworksSequence(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnFirework();
            yield return new WaitForSeconds(0.5f);  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        }
    }

    private void SpawnFirework()
    {
        Camera cam = Camera.main;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float margin = 50f; 

        float randomX = Random.Range(margin, screenWidth - margin);
        float randomY = Random.Range(screenHeight / 2f, screenHeight - margin);

        Vector3 screenPos = new Vector3(randomX, randomY, cam.nearClipPlane + 5f);  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = -5f;  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B

        GameObject fw = Instantiate(fireworkPrefab, worldPos, Quaternion.identity, fireworksParent);
        Destroy(fw, 5f);  //�}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B
    }
}
