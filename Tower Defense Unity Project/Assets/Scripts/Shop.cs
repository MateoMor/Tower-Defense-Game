using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    
    // Referencias a los botones en la UI para cambiar su apariencia
    public Button standardTurretButton;
    public Button missileLauncherButton;
    public Button laserBeamerButton;
    
    // Color para los botones deshabilitados
    public Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    
    // Nueva propiedad para controlar disponibilidad de Fireball
    public bool fireballAvailable = true;
    
    // Referencia al botón de Fireball en la UI
    public Button fireballButton;
    
    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
        UpdateButtonVisuals();
    }
    
    // Actualiza la apariencia visual de los botones según su disponibilidad
    void UpdateButtonVisuals()
    {
        // Standard Turret
        if (standardTurretButton != null)
        {
            ColorBlock colors = standardTurretButton.colors;
            if (!standardTurret.available)
            {
                colors.normalColor = disabledColor;
                colors.highlightedColor = disabledColor;
                colors.pressedColor = disabledColor;
                colors.selectedColor = disabledColor;
            }
            standardTurretButton.colors = colors;
        }
        
        // Missile Launcher
        if (missileLauncherButton != null)
        {
            ColorBlock colors = missileLauncherButton.colors;
            if (!missileLauncher.available)
            {
                colors.normalColor = disabledColor;
                colors.highlightedColor = disabledColor;
                colors.pressedColor = disabledColor;
                colors.selectedColor = disabledColor;
            }
            missileLauncherButton.colors = colors;
        }
        
        // Laser Beamer
        if (laserBeamerButton != null)
        {
            ColorBlock colors = laserBeamerButton.colors;
            if (!laserBeamer.available)
            {
                colors.normalColor = disabledColor;
                colors.highlightedColor = disabledColor;
                colors.pressedColor = disabledColor;
                colors.selectedColor = disabledColor;
            }
            laserBeamerButton.colors = colors;
        }
        
        // Fireball Spell
        if (fireballButton != null)
        {
            ColorBlock colors = fireballButton.colors;
            if (!fireballAvailable)
            {
                colors.normalColor = disabledColor;
                colors.highlightedColor = disabledColor;
                colors.pressedColor = disabledColor;
                colors.selectedColor = disabledColor;
            }
            fireballButton.colors = colors;
        }
    }

    public void SelectStandardTurret ()
    {
        // Verificar si está disponible antes de seleccionarlo
        if (!standardTurret.available)
        {
            return;
        }
        
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        // Verificar si está disponible antes de seleccionarlo
        if (!missileLauncher.available)
        {
            return;
        }
        
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        // Verificar si está disponible antes de seleccionarlo
        if (!laserBeamer.available)
        {
            return;
        }
        
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    public void SelectFireball()
    {
        // Verificar si está disponible antes de seleccionarlo
        if (!fireballAvailable)
        {
            return;
        }
        
        BuildManager.instance.SelectTurretToBuild(null);
        
        bool currentMode = FireballTargeting.Instance.isActive;
        FireballTargeting.Instance.ToggleFireballMode(!currentMode);
    }
}
