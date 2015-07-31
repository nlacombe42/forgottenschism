package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.Screen;
import net.forgottenschism.gui.Size2d;
import net.forgottenschism.gui.Window;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;

import java.util.LinkedList;
import java.util.List;

public abstract class AbstractScreen implements Screen
{
    private List<Window> windows;
    private boolean enabled;
    private boolean visible;
    private Size2d size;
	private GameContainer gameContainer;

    public AbstractScreen()
    {
        windows = new LinkedList<>();
        enabled = false;
        visible = false;
        size = new Size2d(0, 0);

        Window mainWindow = new WindowImpl(this, true);

        showWindow(mainWindow);
    }

    protected void addToMainWindow(Control control)
    {
        getMainWindow().addControl(control);
    }

	@Override
	public void init(GameContainer gameContainer)
	{
		this.gameContainer = gameContainer;
	}

	@Override
	public void setScreenSize(Size2d size)
    {
        this.size = size;

        getMainWindow().setSize(size);
    }

    @Override
    public Size2d getScreenSize()
    {
        return size;
    }

    @Override
    public void showWindow(Window window)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.setFocus(false);

        windows.add(window);

        window.setFocus(true);
    }

    @Override
    public void closeWindow(Window window)
    {
        windows.remove(window);

        if(!windows.isEmpty())
            getActiveWindow().setFocus(true);
    }

    private Window getActiveWindow()
    {
        if(windows.isEmpty())
            return null;

        return windows.get(windows.size()-1);
    }

    protected Window getMainWindow()
    {
        return windows.get(0);
    }

    @Override
    public void enterScreen()
    {
        enabled = true;
        visible = true;
    }

    @Override
    public void pauseScreen()
    {
        enabled = false;
        visible = false;
    }

    @Override
    public void resumeScreen()
    {
        enabled = true;
        visible = true;
    }

    @Override
    public void leaveScreen()
    {
        enabled = false;
        visible = false;
    }

    @Override
    public void keyReleased(int key, char character)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.keyReleased(key, character);
    }

    @Override
    public void keyPressed(int key, char character)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.keyPressed(key, character);
    }

    @Override
	public void render(Graphics graphics)
	{
        if(!visible)
            return;

        for(Window window : windows)
			window.render(graphics);
	}

    @Override
	public void update(int delta)
	{
        if(!enabled)
            return;

        for(Window window : windows)
			window.update(delta);
	}
}
