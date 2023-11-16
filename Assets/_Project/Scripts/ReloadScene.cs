using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // Вызывается при нажатии кнопки или другого события, которое должно вызвать перезагрузку сцены
    public void Restart()
    {
        // Получаем имя текущей загруженной сцены
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneName);
    }
}
