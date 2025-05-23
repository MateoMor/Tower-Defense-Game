using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	public GameObject ui;

	public Text upgradeCost;
	public Button upgradeButton;

	public Text sellAmount;
	
	// Si las mejoras están habilitadas globalmente
	public bool upgradesEnabled = true;

	private Node target;
	public void SetTarget (Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		if (!target.isUpgraded)
		{
			upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
			// Solo permite mejorar si las mejoras están habilitadas globalmente
			upgradeButton.interactable = upgradesEnabled;
			
			// Mostrar texto diferente si las mejoras están deshabilitadas
			if (!upgradesEnabled)
			{
				upgradeCost.text = "LOCKED";
			}
		} 
		else
		{
			upgradeCost.text = "DONE";
			upgradeButton.interactable = false;
		}

		sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

		ui.SetActive(true);
	}

	public void Hide ()
	{
		ui.SetActive(false);
	}
	public void Upgrade ()
	{
		// Verificar si las mejoras están habilitadas
		if (!upgradesEnabled)
		{
			Debug.Log("Las mejoras están deshabilitadas en este nivel");
			return;
		}
		
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}
	public void Sell ()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}
	
	// Método para habilitar o deshabilitar las mejoras
	public void SetUpgradesEnabled(bool enabled)
	{
		upgradesEnabled = enabled;
		
		// Si hay un nodo seleccionado, actualizar su UI
		if (target != null)
		{
			SetTarget(target);
		}
	}

}
