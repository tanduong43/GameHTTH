// HTTH VISUAL MAP STUDIO ENGINE - PRO v2.0
// Fully integrated with 224 Game Maps & Server Assets
(function () {
    'use strict';

    // CONSTANTS & CONFIG
    const TILE_SIZE = 24;
    const TREASURE_CENTER_X = 350;
    const TREASURE_CENTER_Y = 250;

    // STATE
    const state = {
        mapId: 513,
        mapName: 'Bảo Vệ Kho Báu Namie',
        cols: 35,
        rows: 17,
        tileSetId: 5, // 5 = Sand, 1 = Grass, 2 = Stone, 4 = Town
        zoom: 1.0,
        currentTool: 'select', // 'select', 'stamp', 'eraser'
        activeTerrain: 'sand',
        brushSize: 1,
        selectedPaletteItem: null,
        selectedObject: null,
        selectedEntity: null, // Selected NPC, Mob, or Vgo
        isDragging: false,
        dragOffsetX: 0,
        dragOffsetY: 0,
        showGrid: true,
        showObjects: true,
        showNpcs: true,
        showMobs: true,
        showVgos: true,
        showDefenseGuides: true,
        bgId: 7,
        mapBackStr: '[7,270,984,408]',
        tiles: [], // 2D array of tile indices [rows][cols]
        placedObjects: [], // Array of { id, iconId, imgId, name, templateId, x, y, tileX, tileY, dx, dy, layer, width, height }
        npcs: [], // Array of { iditem, name, namegt, chat, x, y, typeIcon }
        mobs: [], // Array of { mobId, x, y, index }
        vgos: [], // Array of { id_map_go, xold, yold, xnew, ynew }
        history: [],
        historyIndex: -1,
        imagesCache: {}
    };

    // ITEM CATALOG DEFINITIONS
    const ITEM_CATALOG = [
        { iconId: 81, templateId: 581, name: 'Rương Kho Báu', layer: 3, width: 48, height: 36, defaultX: 336, defaultY: 240 },
        { iconId: 82, templateId: 582, name: 'Cờ Hải Tặc', layer: 3, width: 32, height: 48, defaultX: 240, defaultY: 240 },
        { iconId: 80, templateId: 580, name: 'Nhà / Thuyền Hải Tặc', layer: 3, width: 96, height: 80, defaultX: 336, defaultY: 144 },
        { iconId: 83, templateId: 583, name: 'Rào Chắn Gai', layer: 3, width: 48, height: 40, defaultX: 144, defaultY: 264 },
        { iconId: 84, templateId: 584, name: 'Pháo Thủ / Đại Bác', layer: 3, width: 48, height: 40, defaultX: 72, defaultY: 264 }
    ];

    // DYNAMIC ASSET HELPERS
    function getTileImage(tileSetId, tileVal) {
        const key = `tile_${tileSetId}_${tileVal}`;
        if (!state.imagesCache[key]) {
            const img = new Image();
            img.src = `assets/tiles/${tileSetId}$${tileVal}.png`;
            img.onload = () => requestRedraw();
            state.imagesCache[key] = img;
        }
        return state.imagesCache[key];
    }

    function getBgImage(bgId) {
        const key = `bg_${bgId}`;
        if (!state.imagesCache[key]) {
            const img = new Image();
            if (window.EMBEDDED_ASSETS && window.EMBEDDED_ASSETS[`bg_${bgId}`]) {
                img.src = window.EMBEDDED_ASSETS[`bg_${bgId}`];
            } else {
                img.src = `assets/bg/${bgId}.png`;
                img.onerror = () => {
                    img.src = 'assets/bg_7.png';
                };
            }
            img.onload = () => requestRedraw();
            state.imagesCache[key] = img;
        }
        return state.imagesCache[key];
    }

    function getIconImage(iconId) {
        const key = `icon_${iconId}`;
        if (!state.imagesCache[key]) {
            const img = new Image();
            if (window.EMBEDDED_ASSETS && window.EMBEDDED_ASSETS[`icon_${iconId}`]) {
                img.src = window.EMBEDDED_ASSETS[`icon_${iconId}`];
            } else {
                img.src = `assets/icons/${iconId}.png`;
                img.onerror = () => {
                    img.src = `assets/${iconId}.png`;
                };
            }
            img.onload = () => requestRedraw();
            state.imagesCache[key] = img;
        }
        return state.imagesCache[key];
    }

    // DOM ELEMENTS
    let canvas, ctx, canvasMount, canvasScrollWrapper;
    let mapIdInput, mapNameInput, mapSizeSelect;
    let itemPaletteEl, placedObjectsListEl, inspectorFormEl, emptySelectionMsgEl;
    let sqlOutputTextareaEl, exportModalEl, presetsModalEl, gameMapsModalEl, toastEl;
    let importModalEl, importInputTextareaEl;

    // INITIALIZATION
    window.addEventListener('DOMContentLoaded', () => {
        initDOMElements();
        initCanvas();
        loadAssetsImages();
        setupEventListeners();
        
        // Auto-load Map 513 from catalog if available, or preset
        if (window.MAPS_CATALOG && window.MAPS_CATALOG.length) {
            loadGameMapFromCatalog(513);
        } else {
            loadPreset('nami_orange');
        }

        renderPalette();
        renderGameMapsCatalog();
        updateUI();
    });

    function initDOMElements() {
        canvas = document.getElementById('mapCanvas');
        ctx = canvas.getContext('2d');
        canvasMount = document.getElementById('canvasMount');
        canvasScrollWrapper = document.getElementById('canvasScrollWrapper');

        mapIdInput = document.getElementById('mapIdInput');
        mapNameInput = document.getElementById('mapNameInput');
        mapSizeSelect = document.getElementById('mapSizeSelect');

        itemPaletteEl = document.getElementById('itemPalette');
        placedObjectsListEl = document.getElementById('placedObjectsList');
        inspectorFormEl = document.getElementById('inspectorForm');
        emptySelectionMsgEl = document.getElementById('emptySelectionMsg');

        sqlOutputTextareaEl = document.getElementById('sqlOutputTextarea');
        exportModalEl = document.getElementById('exportModal');
        presetsModalEl = document.getElementById('presetsModal');
        gameMapsModalEl = document.getElementById('gameMapsModal');
        importModalEl = document.getElementById('importModal');
        importInputTextareaEl = document.getElementById('importInputTextarea');
        toastEl = document.getElementById('appToast');
    }

    function initCanvas() {
        canvas.width = state.cols * TILE_SIZE;
        canvas.height = state.rows * TILE_SIZE;
        initTileGrid();
    }

    function initTileGrid() {
        state.tiles = [];
        for (let r = 0; r < state.rows; r++) {
            const row = [];
            for (let c = 0; c < state.cols; c++) {
                row.push(r < 4 ? 0 : 7);
            }
            state.tiles.push(row);
        }
    }

    function resizeCanvas(newCols, newRows) {
        const oldTiles = state.tiles;
        state.cols = newCols;
        state.rows = newRows;
        canvas.width = newCols * TILE_SIZE;
        canvas.height = newRows * TILE_SIZE;

        state.tiles = [];
        for (let r = 0; r < newRows; r++) {
            const row = [];
            for (let c = 0; c < newCols; c++) {
                if (oldTiles[r] && oldTiles[r][c] !== undefined) {
                    row.push(oldTiles[r][c]);
                } else {
                    row.push(r < 4 ? 0 : 7);
                }
            }
            state.tiles.push(row);
        }

        applyZoom();
        requestRedraw();
    }

    function loadAssetsImages() {
        ITEM_CATALOG.forEach(item => {
            getIconImage(item.iconId);
        });
        getBgImage(state.bgId);
    }

    function renderPalette() {
        if (!itemPaletteEl) return;
        itemPaletteEl.innerHTML = '';
        ITEM_CATALOG.forEach(item => {
            const el = document.createElement('div');
            el.className = `palette-item ${state.selectedPaletteItem === item ? 'active' : ''}`;
            el.dataset.id = item.iconId;

            const iconImg = getIconImage(item.iconId);
            const imgSrc = iconImg.src || `assets/icons/${item.iconId}.png`;

            el.innerHTML = `
                <div class="palette-item-thumb">
                    <img src="${imgSrc}" alt="${item.name}">
                </div>
                <div class="palette-item-name">${item.name}</div>
                <div class="palette-item-sub">Icon #${item.iconId} | ${item.width}x${item.height}</div>
            `;

            el.addEventListener('click', () => {
                document.querySelectorAll('.palette-item').forEach(i => i.classList.remove('active'));
                el.classList.add('active');
                state.selectedPaletteItem = item;
                setTool('stamp');
            });

            itemPaletteEl.appendChild(el);
        });
    }

    function setTool(toolName) {
        state.currentTool = toolName;
        document.querySelectorAll('.tool-btn').forEach(btn => btn.classList.remove('active'));
        if (toolName === 'select') document.getElementById('toolSelect')?.classList.add('active');
        if (toolName === 'stamp') document.getElementById('toolStamp')?.classList.add('active');
        if (toolName === 'eraser') document.getElementById('toolEraser')?.classList.add('active');
    }

    // DRAWING PIPELINE
    let redrawRequested = false;
    function requestRedraw() {
        if (!redrawRequested) {
            redrawRequested = true;
            requestAnimationFrame(() => {
                renderMap();
                redrawRequested = false;
            });
        }
    }

    function renderMap() {
        if (!ctx) return;
        const w = state.cols * TILE_SIZE;
        const h = state.rows * TILE_SIZE;

        ctx.clearRect(0, 0, w, h);

        // 1. Draw Background
        const bgImg = getBgImage(state.bgId);
        if (bgImg && bgImg.complete && bgImg.naturalWidth > 0) {
            const bgW = bgImg.naturalWidth;
            const bgH = bgImg.naturalHeight;
            for (let x = 0; x < w; x += bgW) {
                for (let y = 0; y < h; y += bgH) {
                    ctx.drawImage(bgImg, x, y);
                }
            }
        } else {
            // High-res gradient fallback
            const grad = ctx.createLinearGradient(0, 0, 0, h);
            if (state.activeTerrain === 'sand') {
                grad.addColorStop(0, '#7dd3fc');
                grad.addColorStop(0.35, '#bae6fd');
                grad.addColorStop(0.35, '#fde047');
                grad.addColorStop(1, '#ca8a04');
            } else if (state.activeTerrain === 'grass') {
                grad.addColorStop(0, '#38bdf8');
                grad.addColorStop(0.35, '#93c5fd');
                grad.addColorStop(0.35, '#86efac');
                grad.addColorStop(1, '#15803d');
            } else {
                grad.addColorStop(0, '#64748b');
                grad.addColorStop(0.35, '#94a3b8');
                grad.addColorStop(0.35, '#cbd5e1');
                grad.addColorStop(1, '#475569');
            }
            ctx.fillStyle = grad;
            ctx.fillRect(0, 0, w, h);
        }

        // 2. Draw Tiles
        for (let r = 0; r < state.rows; r++) {
            for (let c = 0; c < state.cols; c++) {
                const tileVal = state.tiles[r] ? state.tiles[r][c] : 0;
                if (tileVal > 0) {
                    const tileImg = getTileImage(state.tileSetId, tileVal);
                    if (tileImg && tileImg.complete && tileImg.naturalWidth > 0) {
                        ctx.drawImage(tileImg, c * TILE_SIZE, r * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    } else {
                        // Fallback block color
                        ctx.fillStyle = state.activeTerrain === 'sand' ? '#eab308' : '#22c55e';
                        ctx.fillRect(c * TILE_SIZE, r * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                        ctx.strokeStyle = 'rgba(0,0,0,0.15)';
                        ctx.strokeRect(c * TILE_SIZE, r * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    }
                }
            }
        }

        // 3. Draw Placed Objects (ItemMap)
        if (state.showObjects) {
            const sortedObjects = [...state.placedObjects].sort((a, b) => (a.layer || 3) - (b.layer || 3) || a.y - b.y);
            for (const obj of sortedObjects) {
                const isSelected = state.selectedObject && state.selectedObject.id === obj.id;
                const img = getIconImage(obj.imgId !== undefined ? obj.imgId : obj.iconId);
                const width = (img && img.complete && img.naturalWidth) ? img.naturalWidth : (obj.width || 48);
                const height = (img && img.complete && img.naturalHeight) ? img.naturalHeight : (obj.height || 48);

                const drawX = obj.x + (obj.dx || 0);
                const drawY = obj.y + (obj.dy || 0);

                if (img && img.complete && img.naturalWidth > 0) {
                    ctx.drawImage(img, drawX, drawY, width, height);

                    // Selection Box & Glow
                    if (isSelected) {
                        ctx.strokeStyle = '#ffaa00';
                        ctx.lineWidth = 2;
                        ctx.setLineDash([4, 2]);
                        ctx.strokeRect(drawX - 2, drawY - 2, width + 4, height + 4);
                        ctx.setLineDash([]);

                        // Position anchor marker
                        ctx.fillStyle = '#ffaa00';
                        ctx.beginPath();
                        ctx.arc(obj.x, obj.y, 4, 0, Math.PI * 2);
                        ctx.fill();
                    }
                } else {
                    ctx.fillStyle = '#e67e22';
                    ctx.fillRect(drawX, drawY, width, height);
                }
            }
        }

        // 4. Draw NPCs
        if (state.showNpcs && state.npcs && state.npcs.length > 0) {
            for (const npc of state.npcs) {
                const nx = npc.x;
                const ny = npc.y;
                const isSel = state.selectedEntity && state.selectedEntity.type === 'npc' && state.selectedEntity.data === npc;

                // NPC Avatar circle marker
                ctx.save();
                ctx.fillStyle = isSel ? '#38bdf8' : '#0284c7';
                ctx.beginPath();
                ctx.arc(nx, ny, 16, 0, Math.PI * 2);
                ctx.fill();
                ctx.lineWidth = 2;
                ctx.strokeStyle = '#fff';
                ctx.stroke();

                // NPC Head/Face icon placeholder
                ctx.fillStyle = '#fff';
                ctx.font = 'bold 10px Outfit, sans-serif';
                ctx.textAlign = 'center';
                ctx.fillText('NPC', nx, ny + 3);

                // NPC Name Pill
                const nameText = npc.name || 'NPC';
                ctx.font = 'bold 11px Outfit, sans-serif';
                const textWidth = ctx.measureText(nameText).width;
                const pillW = textWidth + 16;
                const pillH = 18;
                const pillX = nx - pillW / 2;
                const pillY = ny - 28;

                ctx.fillStyle = 'rgba(15, 23, 42, 0.85)';
                ctx.roundRect ? ctx.roundRect(pillX, pillY, pillW, pillH, 9) : ctx.fillRect(pillX, pillY, pillW, pillH);
                ctx.fill();
                ctx.strokeStyle = '#38bdf8';
                ctx.lineWidth = 1;
                ctx.stroke();

                ctx.fillStyle = '#38bdf8';
                ctx.fillText(nameText, nx, pillY + 13);

                // NPC Subtitle / Greeting
                if (npc.namegt && npc.namegt.trim()) {
                    ctx.font = '9px Outfit, sans-serif';
                    ctx.fillStyle = '#fde047';
                    ctx.fillText(`(${npc.namegt.replace(/\\n/g, ' ')})`, nx, pillY - 4);
                }
                ctx.restore();
            }
        }

        // 5. Draw Mobs
        if (state.showMobs && state.mobs && state.mobs.length > 0) {
            for (const mob of state.mobs) {
                const mx = mob.x;
                const my = mob.y;
                const isSel = state.selectedEntity && state.selectedEntity.type === 'mob' && state.selectedEntity.data === mob;

                ctx.save();
                ctx.fillStyle = isSel ? '#ef4444' : '#b91c1c';
                ctx.beginPath();
                ctx.arc(mx, my, 14, 0, Math.PI * 2);
                ctx.fill();
                ctx.lineWidth = 2;
                ctx.strokeStyle = '#fca5a5';
                ctx.stroke();

                ctx.fillStyle = '#fff';
                ctx.font = 'bold 9px Outfit, sans-serif';
                ctx.textAlign = 'center';
                ctx.fillText(`M#${mob.mobId}`, mx, my + 3);

                // Badge
                ctx.fillStyle = 'rgba(0,0,0,0.7)';
                ctx.fillRect(mx - 24, my - 24, 48, 14);
                ctx.fillStyle = '#f87171';
                ctx.font = 'bold 9px monospace';
                ctx.fillText(`Quái #${mob.mobId}`, mx, my - 14);
                ctx.restore();
            }
        }

        // 6. Draw Portals / Vgos
        if (state.showVgos && state.vgos && state.vgos.length > 0) {
            for (const vgo of state.vgos) {
                const vx = vgo.xold;
                const vy = vgo.yold;
                const isSel = state.selectedEntity && state.selectedEntity.type === 'vgo' && state.selectedEntity.data === vgo;

                ctx.save();
                // Glowing Portal gate
                ctx.strokeStyle = isSel ? '#c084fc' : '#a855f7';
                ctx.lineWidth = 3;
                ctx.setLineDash([4, 4]);
                ctx.beginPath();
                ctx.arc(vx, vy, 20, 0, Math.PI * 2);
                ctx.stroke();
                ctx.setLineDash([]);

                ctx.fillStyle = 'rgba(168, 85, 247, 0.3)';
                ctx.beginPath();
                ctx.arc(vx, vy, 18, 0, Math.PI * 2);
                ctx.fill();

                // Target Map Tag
                const targetText = `🚪 Sang Map #${vgo.id_map_go}`;
                ctx.font = 'bold 10px Outfit, sans-serif';
                ctx.textAlign = 'center';
                const tagW = ctx.measureText(targetText).width + 12;
                ctx.fillStyle = 'rgba(15, 23, 42, 0.9)';
                ctx.fillRect(vx - tagW / 2, vy - 32, tagW, 16);
                ctx.strokeStyle = '#c084fc';
                ctx.lineWidth = 1;
                ctx.strokeRect(vx - tagW / 2, vy - 32, tagW, 16);

                ctx.fillStyle = '#d8b4fe';
                ctx.fillText(targetText, vx, vy - 20);
                ctx.restore();
            }
        }

        // 7. Draw Defense Guides (Center Treasure Radius, Monster Waves)
        if (state.showDefenseGuides) {
            ctx.save();
            // Defense Center Circle
            ctx.strokeStyle = 'rgba(255, 170, 0, 0.6)';
            ctx.lineWidth = 2;
            ctx.setLineDash([6, 6]);
            ctx.beginPath();
            ctx.arc(TREASURE_CENTER_X, TREASURE_CENTER_Y, 70, 0, Math.PI * 2);
            ctx.stroke();

            // Center Point Flag
            ctx.fillStyle = '#ff5500';
            ctx.beginPath();
            ctx.arc(TREASURE_CENTER_X, TREASURE_CENTER_Y, 5, 0, Math.PI * 2);
            ctx.fill();

            // Monster Path Arrows (Left & Right)
            ctx.strokeStyle = 'rgba(239, 68, 68, 0.7)';
            ctx.lineWidth = 3;
            ctx.setLineDash([8, 4]);

            // Left Wave Path
            ctx.beginPath();
            ctx.moveTo(30, TREASURE_CENTER_Y);
            ctx.lineTo(TREASURE_CENTER_X - 60, TREASURE_CENTER_Y);
            ctx.stroke();

            // Right Wave Path
            ctx.beginPath();
            ctx.moveTo(w - 30, TREASURE_CENTER_Y);
            ctx.lineTo(TREASURE_CENTER_X + 60, TREASURE_CENTER_Y);
            ctx.stroke();
            ctx.setLineDash([]);

            // Labels
            ctx.fillStyle = '#ffffff';
            ctx.font = '10px Outfit, sans-serif';
            ctx.fillText('ĐIỂM THỦ THÀNH (350, 250)', TREASURE_CENTER_X - 65, TREASURE_CENTER_Y - 80);
            ctx.fillText('◀ QUÁI TIẾN VÀO', 40, TREASURE_CENTER_Y - 10);
            ctx.fillText('QUÁI TIẾN VÀO ▶', w - 120, TREASURE_CENTER_Y - 10);
            ctx.restore();
        }

        // 8. Grid Overlay
        if (state.showGrid) {
            ctx.strokeStyle = 'rgba(255, 255, 255, 0.12)';
            ctx.lineWidth = 1;
            for (let x = 0; x <= w; x += TILE_SIZE) {
                ctx.beginPath();
                ctx.moveTo(x, 0);
                ctx.lineTo(x, h);
                ctx.stroke();
            }
            for (let y = 0; y <= h; y += TILE_SIZE) {
                ctx.beginPath();
                ctx.moveTo(0, y);
                ctx.lineTo(w, y);
                ctx.stroke();
            }
        }
    }

    // CANVAS MOUSE EVENTS
    function setupEventListeners() {
        canvas.addEventListener('mousedown', onCanvasMouseDown);
        window.addEventListener('mousemove', onCanvasMouseMove);
        window.addEventListener('mouseup', onCanvasMouseUp);

        // Sidebar Tabs
        document.querySelectorAll('.tab-btn').forEach(btn => {
            btn.addEventListener('click', () => {
                document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
                document.querySelectorAll('.tab-panel').forEach(p => p.classList.remove('active'));
                btn.classList.add('active');
                const targetPanel = document.getElementById(btn.dataset.tab);
                if (targetPanel) targetPanel.classList.add('active');
            });
        });

        // Tool buttons
        document.getElementById('toolSelect')?.addEventListener('click', () => setTool('select'));
        document.getElementById('toolStamp')?.addEventListener('click', () => setTool('stamp'));
        document.getElementById('toolEraser')?.addEventListener('click', () => setTool('eraser'));

        // Terrain theme cards
        document.querySelectorAll('.terrain-card').forEach(card => {
            card.addEventListener('click', () => {
                document.querySelectorAll('.terrain-card').forEach(c => c.classList.remove('active'));
                card.classList.add('active');
                state.activeTerrain = card.dataset.terrain;
                requestRedraw();
            });
        });

        // Background cards
        document.querySelectorAll('.bg-card').forEach(card => {
            card.addEventListener('click', () => {
                document.querySelectorAll('.bg-card').forEach(c => c.classList.remove('active'));
                card.classList.add('active');
                state.bgId = parseInt(card.dataset.bg);
                state.mapBackStr = card.dataset.backStr;
                requestRedraw();
            });
        });

        // Zoom Controls
        document.getElementById('btnZoomIn')?.addEventListener('click', () => { setZoom(state.zoom + 0.25); });
        document.getElementById('btnZoomOut')?.addEventListener('click', () => { setZoom(state.zoom - 0.25); });
        document.getElementById('btnZoomReset')?.addEventListener('click', () => { setZoom(1.0); });

        // Toggles
        document.getElementById('chkShowGrid')?.addEventListener('change', (e) => {
            state.showGrid = e.target.checked;
            requestRedraw();
        });
        document.getElementById('chkShowObjects')?.addEventListener('change', (e) => {
            state.showObjects = e.target.checked;
            requestRedraw();
        });
        document.getElementById('chkShowNpcs')?.addEventListener('change', (e) => {
            state.showNpcs = e.target.checked;
            requestRedraw();
        });
        document.getElementById('chkShowMobs')?.addEventListener('change', (e) => {
            state.showMobs = e.target.checked;
            requestRedraw();
        });
        document.getElementById('chkShowVgos')?.addEventListener('change', (e) => {
            state.showVgos = e.target.checked;
            requestRedraw();
        });
        document.getElementById('chkShowDefenseGuides')?.addEventListener('change', (e) => {
            state.showDefenseGuides = e.target.checked;
            requestRedraw();
        });

        // Map Size Select
        mapSizeSelect?.addEventListener('change', (e) => {
            const [c, r] = e.target.value.split('x').map(Number);
            resizeCanvas(c, r);
            requestRedraw();
        });

        // Quick Load Map ID
        document.getElementById('btnQuickLoadMapId')?.addEventListener('click', () => {
            const mid = parseInt(mapIdInput.value);
            if (!isNaN(mid)) loadGameMapFromCatalog(mid);
        });
        mapIdInput?.addEventListener('keydown', (e) => {
            if (e.key === 'Enter') {
                const mid = parseInt(mapIdInput.value);
                if (!isNaN(mid)) loadGameMapFromCatalog(mid);
            }
        });

        // Map Browser Modal
        document.getElementById('btnBrowseGameMaps')?.addEventListener('click', () => {
            if (gameMapsModalEl) gameMapsModalEl.classList.remove('hidden');
        });
        document.getElementById('btnCloseGameMapsModal')?.addEventListener('click', () => {
            if (gameMapsModalEl) gameMapsModalEl.classList.add('hidden');
        });
        document.getElementById('btnCloseGameMapsModalBtn')?.addEventListener('click', () => {
            if (gameMapsModalEl) gameMapsModalEl.classList.add('hidden');
        });

        // Search & Filter in Map Browser
        document.getElementById('catalogSearchInput')?.addEventListener('input', (e) => {
            filterGameMapsCatalog(e.target.value);
        });

        document.querySelectorAll('.catalog-chip').forEach(chip => {
            chip.addEventListener('click', () => {
                document.querySelectorAll('.catalog-chip').forEach(c => c.classList.remove('active'));
                chip.classList.add('active');
                filterGameMapsCatalog(document.getElementById('catalogSearchInput')?.value || '', chip.dataset.cat);
            });
        });

        // Inspector Inputs
        document.getElementById('propPixelX')?.addEventListener('input', (e) => {
            if (state.selectedObject) {
                state.selectedObject.x = parseInt(e.target.value) || 0;
                state.selectedObject.tileX = Math.round(state.selectedObject.x / TILE_SIZE);
                document.getElementById('propTileX').value = state.selectedObject.tileX;
                requestRedraw();
            }
        });
        document.getElementById('propPixelY')?.addEventListener('input', (e) => {
            if (state.selectedObject) {
                state.selectedObject.y = parseInt(e.target.value) || 0;
                state.selectedObject.tileY = Math.round(state.selectedObject.y / TILE_SIZE);
                document.getElementById('propTileY').value = state.selectedObject.tileY;
                requestRedraw();
            }
        });
        document.getElementById('propLayer')?.addEventListener('change', (e) => {
            if (state.selectedObject) {
                state.selectedObject.layer = parseInt(e.target.value);
                requestRedraw();
            }
        });

        // Inspector Actions
        document.getElementById('btnDuplicateSelected')?.addEventListener('click', () => {
            if (state.selectedObject) {
                const dup = addObject(
                    state.selectedObject.iconId,
                    state.selectedObject.templateId,
                    state.selectedObject.name,
                    state.selectedObject.x + 24,
                    state.selectedObject.y,
                    state.selectedObject.layer
                );
                selectObject(dup);
                requestRedraw();
                updatePlacedObjectsList();
                showToast('Đã nhân bản vật thể!');
            }
        });
        document.getElementById('btnDeleteSelected')?.addEventListener('click', () => {
            if (state.selectedObject) {
                state.placedObjects = state.placedObjects.filter(o => o.id !== state.selectedObject.id);
                selectObject(null);
                requestRedraw();
                updatePlacedObjectsList();
                showToast('Đã xóa vật thể!');
            }
        });

        document.getElementById('btnSnapToCenter')?.addEventListener('click', () => {
            if (state.selectedObject) {
                state.selectedObject.x = TREASURE_CENTER_X;
                state.selectedObject.y = TREASURE_CENTER_Y;
                state.selectedObject.tileX = Math.round(TREASURE_CENTER_X / TILE_SIZE);
                state.selectedObject.tileY = Math.round(TREASURE_CENTER_Y / TILE_SIZE);
                selectObject(state.selectedObject);
                requestRedraw();
            }
        });

        document.getElementById('btnSnapToGrid')?.addEventListener('click', () => {
            if (state.selectedObject) {
                state.selectedObject.tileX = Math.round(state.selectedObject.x / TILE_SIZE);
                state.selectedObject.tileY = Math.round(state.selectedObject.y / TILE_SIZE);
                state.selectedObject.x = state.selectedObject.tileX * TILE_SIZE;
                state.selectedObject.y = state.selectedObject.tileY * TILE_SIZE;
                selectObject(state.selectedObject);
                requestRedraw();
            }
        });

        // Import Modal & Samples
        document.getElementById('btnImportDataModal')?.addEventListener('click', () => importModalEl.classList.remove('hidden'));
        document.getElementById('btnCloseImportModal')?.addEventListener('click', () => importModalEl.classList.add('hidden'));
        document.getElementById('btnCancelImport')?.addEventListener('click', () => importModalEl.classList.add('hidden'));
        document.getElementById('btnClearImportText')?.addEventListener('click', () => {
            if (importInputTextareaEl) importInputTextareaEl.value = '';
        });
        document.getElementById('btnExecuteImport')?.addEventListener('click', () => {
            if (importInputTextareaEl) {
                const ok = parseAndImportData(importInputTextareaEl.value);
                if (ok) importModalEl.classList.add('hidden');
            }
        });

        // Quick Samples
        document.getElementById('btnSampleMap513')?.addEventListener('click', () => loadGameMapFromCatalog(513));
        document.getElementById('btnSampleMap45')?.addEventListener('click', () => loadGameMapFromCatalog(45));
        document.getElementById('btnSampleMap41')?.addEventListener('click', () => loadGameMapFromCatalog(41));

        // Modals & Export
        document.getElementById('btnExportSqlModal')?.addEventListener('click', openExportModal);
        document.getElementById('btnCloseExportModal')?.addEventListener('click', () => exportModalEl.classList.add('hidden'));
        document.getElementById('btnConfirmCloseExport')?.addEventListener('click', () => exportModalEl.classList.add('hidden'));

        document.getElementById('btnCopySql')?.addEventListener('click', copySqlToClipboard);
        document.getElementById('btnDownloadSql')?.addEventListener('click', downloadSqlFile);
        document.getElementById('btnDownloadJson')?.addEventListener('click', downloadProjectJson);

        // Add custom icon ID
        document.getElementById('btnAddCustomIcon')?.addEventListener('click', () => {
            const idInput = document.getElementById('customIconIdInput');
            const iconId = parseInt(idInput.value);
            if (iconId >= 0) {
                const newItem = {
                    iconId: iconId,
                    templateId: 500 + iconId,
                    name: `Icon #${iconId}`,
                    layer: 3,
                    width: 48,
                    height: 48
                };
                ITEM_CATALOG.push(newItem);
                renderPalette();
                idInput.value = '';
                showToast(`Đã thêm Icon #${iconId} vào thư viện!`);
            }
        });
    }

    function onCanvasMouseDown(e) {
        const rect = canvas.getBoundingClientRect();
        const mouseX = (e.clientX - rect.left) / state.zoom;
        const mouseY = (e.clientY - rect.top) / state.zoom;

        if (state.currentTool === 'select') {
            // Find clicked object (top-most)
            const clicked = [...state.placedObjects].reverse().find(obj => {
                const img = getIconImage(obj.imgId !== undefined ? obj.imgId : obj.iconId);
                const width = (img && img.complete && img.naturalWidth) ? img.naturalWidth : (obj.width || 48);
                const height = (img && img.complete && img.naturalHeight) ? img.naturalHeight : (obj.height || 48);
                const left = obj.x + (obj.dx || 0);
                const top = obj.y + (obj.dy || 0);
                const right = left + width;
                const bottom = top + height;
                return mouseX >= left && mouseX <= right && mouseY >= top && mouseY <= bottom;
            });

            if (clicked) {
                selectObject(clicked);
                state.isDragging = true;
                state.dragOffsetX = mouseX - clicked.x;
                state.dragOffsetY = mouseY - clicked.y;
                requestRedraw();
                return;
            }

            // Check if clicked an NPC
            const clickedNpc = state.npcs.find(npc => {
                const dist = Math.hypot(mouseX - npc.x, mouseY - npc.y);
                return dist <= 20;
            });
            if (clickedNpc) {
                state.selectedEntity = { type: 'npc', data: clickedNpc };
                showToast(`Đã chọn NPC: ${clickedNpc.name} (${clickedNpc.chat || clickedNpc.namegt})`);
                requestRedraw();
                return;
            }

            // Check if clicked a Mob
            const clickedMob = state.mobs.find(mob => {
                const dist = Math.hypot(mouseX - mob.x, mouseY - mob.y);
                return dist <= 18;
            });
            if (clickedMob) {
                state.selectedEntity = { type: 'mob', data: clickedMob };
                showToast(`Đã chọn Quái ID: #${clickedMob.mobId} tại (${clickedMob.x}, ${clickedMob.y})`);
                requestRedraw();
                return;
            }

            // Check if clicked a Portal
            const clickedVgo = state.vgos.find(vgo => {
                const dist = Math.hypot(mouseX - vgo.xold, mouseY - vgo.yold);
                return dist <= 22;
            });
            if (clickedVgo) {
                state.selectedEntity = { type: 'vgo', data: clickedVgo };
                showToast(`Cổng Dịch Chuyển -> Sang Map #${clickedVgo.id_map_go}`);
                requestRedraw();
                return;
            }

            selectObject(null);
            state.selectedEntity = null;
            requestRedraw();
        } else if (state.currentTool === 'stamp') {
            if (state.selectedPaletteItem) {
                const tileX = Math.floor(mouseX / TILE_SIZE);
                const tileY = Math.floor(mouseY / TILE_SIZE);
                const item = state.selectedPaletteItem;
                const newObj = addObject(
                    item.iconId,
                    item.templateId,
                    item.name,
                    tileX * TILE_SIZE,
                    tileY * TILE_SIZE,
                    item.layer
                );
                selectObject(newObj);
                requestRedraw();
                updatePlacedObjectsList();
                saveState();
            }
        } else if (state.currentTool === 'eraser') {
            const clicked = [...state.placedObjects].reverse().find(obj => {
                const img = getIconImage(obj.imgId !== undefined ? obj.imgId : obj.iconId);
                const width = (img && img.complete && img.naturalWidth) ? img.naturalWidth : (obj.width || 48);
                const height = (img && img.complete && img.naturalHeight) ? img.naturalHeight : (obj.height || 48);
                const left = obj.x + (obj.dx || 0);
                const top = obj.y + (obj.dy || 0);
                return mouseX >= left && mouseX <= left + width && mouseY >= top && mouseY <= top + height;
            });

            if (clicked) {
                state.placedObjects = state.placedObjects.filter(o => o.id !== clicked.id);
                selectObject(null);
                requestRedraw();
                updatePlacedObjectsList();
                saveState();
            }
        }
    }

    function onCanvasMouseMove(e) {
        const rect = canvas.getBoundingClientRect();
        const mouseX = Math.round((e.clientX - rect.left) / state.zoom);
        const mouseY = Math.round((e.clientY - rect.top) / state.zoom);

        // Update Coordinate Status
        const tileX = Math.floor(mouseX / TILE_SIZE);
        const tileY = Math.floor(mouseY / TILE_SIZE);
        const coordEl = document.getElementById('cursorCoordDisplay');
        if (coordEl) {
            coordEl.textContent = `X: ${mouseX}, Y: ${mouseY} (Tile: ${tileX}, ${tileY})`;
        }

        if (state.isDragging && state.selectedObject) {
            state.selectedObject.x = mouseX - state.dragOffsetX;
            state.selectedObject.y = mouseY - state.dragOffsetY;
            state.selectedObject.tileX = Math.round(state.selectedObject.x / TILE_SIZE);
            state.selectedObject.tileY = Math.round(state.selectedObject.y / TILE_SIZE);

            // Update Inspector inputs in real time
            document.getElementById('propPixelX').value = state.selectedObject.x;
            document.getElementById('propPixelY').value = state.selectedObject.y;
            document.getElementById('propTileX').value = state.selectedObject.tileX;
            document.getElementById('propTileY').value = state.selectedObject.tileY;

            requestRedraw();
        }
    }

    function onCanvasMouseUp() {
        if (state.isDragging) {
            state.isDragging = false;
            saveState();
        }
    }

    function addObject(iconId, templateId, name, x, y, layer = 3) {
        const manifest = (window.TEMPLATE_MANIFEST && window.TEMPLATE_MANIFEST[templateId]) || null;
        let imgId = iconId;
        let dx = 0;
        let dy = 0;

        if (manifest) {
            imgId = manifest.imgId;
            dx = manifest.dx || 0;
            dy = manifest.dy || 0;
            if (manifest.layer !== undefined) layer = manifest.layer;
        }

        getIconImage(imgId);

        const obj = {
            id: 'obj_' + Date.now() + '_' + Math.random().toString(36).substr(2, 4),
            iconId: imgId,
            imgId: imgId,
            templateId: templateId || (500 + iconId),
            name: name || `Vật thể #${iconId}`,
            x: x,
            y: y,
            tileX: Math.round(x / TILE_SIZE),
            tileY: Math.round(y / TILE_SIZE),
            dx: dx,
            dy: dy,
            layer: layer,
            width: 48,
            height: 48
        };
        state.placedObjects.push(obj);
        return obj;
    }

    function selectObject(obj) {
        state.selectedObject = obj;
        if (obj) {
            emptySelectionMsgEl.classList.add('hidden');
            inspectorFormEl.classList.remove('hidden');

            document.getElementById('propObjName').textContent = obj.name;
            document.getElementById('propObjIconId').textContent = `Icon #${obj.iconId} | Template #${obj.templateId}`;
            document.getElementById('propObjImg').src = state.imagesCache[`icon_${obj.iconId}`]?.src || `assets/icons/${obj.iconId}.png`;

            document.getElementById('propPixelX').value = obj.x;
            document.getElementById('propPixelY').value = obj.y;
            document.getElementById('propTileX').value = obj.tileX;
            document.getElementById('propTileY').value = obj.tileY;
            document.getElementById('propLayer').value = obj.layer;
        } else {
            emptySelectionMsgEl.classList.remove('hidden');
            inspectorFormEl.classList.add('hidden');
        }
        updatePlacedObjectsListHighlight();
    }

    function updatePlacedObjectsList() {
        if (!placedObjectsListEl) return;
        placedObjectsListEl.innerHTML = '';
        state.placedObjects.forEach(obj => {
            const row = document.createElement('div');
            row.className = `placed-item-row ${state.selectedObject?.id === obj.id ? 'selected' : ''}`;
            row.innerHTML = `
                <span>${obj.name} (#${obj.iconId})</span>
                <span style="font-family:monospace; color:var(--text-muted);">(${obj.x}, ${obj.y})</span>
            `;
            row.addEventListener('click', () => {
                selectObject(obj);
                requestRedraw();
            });
            placedObjectsListEl.appendChild(row);
        });

        document.getElementById('totalObjectsDisplay').textContent = `${state.placedObjects.length} Items`;
    }

    function updatePlacedObjectsListHighlight() {
        const rows = placedObjectsListEl?.querySelectorAll('.placed-item-row');
        if (!rows) return;
        state.placedObjects.forEach((obj, idx) => {
            if (rows[idx]) {
                if (state.selectedObject && state.selectedObject.id === obj.id) {
                    rows[idx].classList.add('selected');
                } else {
                    rows[idx].classList.remove('selected');
                }
            }
        });
    }

    function setZoom(newZoom) {
        state.zoom = Math.max(0.5, Math.min(3.0, newZoom));
        document.getElementById('zoomLevelText').textContent = Math.round(state.zoom * 100) + '%';
        applyZoom();
    }

    function applyZoom() {
        canvas.style.width = (state.cols * TILE_SIZE * state.zoom) + 'px';
        canvas.style.height = (state.rows * TILE_SIZE * state.zoom) + 'px';
    }

    // GAME MAPS CATALOG BROWSER
    function renderGameMapsCatalog(mapsToRender = null) {
        const grid = document.getElementById('gameMapsGrid');
        if (!grid) return;

        const maps = mapsToRender || window.MAPS_CATALOG || [];
        grid.innerHTML = '';

        document.getElementById('catalogCountDisplay').textContent = `${maps.length} Bản Đồ`;

        maps.forEach(map => {
            const card = document.createElement('div');
            card.className = 'map-card-item';

            let npcCount = 0;
            let mobCount = 0;
            let vgoCount = 0;
            let cols = 0;
            let rows = 0;

            try {
                const npcs = JSON.parse(map.npcs || '[]');
                npcCount = npcs.length;
            } catch (e) {}

            try {
                const mobs = JSON.parse(map.mobs || '[]');
                mobCount = mobs.length;
            } catch (e) {}

            try {
                const vgos = JSON.parse(map.vgos || '[]');
                vgoCount = vgos.length;
            } catch (e) {}

            try {
                const dataJson = JSON.parse(map.data || '[]');
                if (Array.isArray(dataJson) && Array.isArray(dataJson[0])) {
                    cols = dataJson[0][0] < 0 ? dataJson[0][0] + 256 : dataJson[0][0];
                    rows = dataJson[0][1] < 0 ? dataJson[0][1] + 256 : dataJson[0][1];
                }
            } catch (e) {}

            const sizeText = (cols && rows) ? `${cols}x${rows} (${cols*24}x${rows*24}px)` : 'Tiêu chuẩn';

            card.innerHTML = `
                <div class="map-card-header">
                    <span class="map-card-id">#${map.id}</span>
                    <span class="map-card-size">${sizeText}</span>
                </div>
                <div class="map-card-title">${map.name}</div>
                <div class="map-card-tags">
                    <span class="map-tag npc">👤 ${npcCount} NPC</span>
                    <span class="map-tag mob">⚔️ ${mobCount} Quái</span>
                    <span class="map-tag vgo">🚪 ${vgoCount} Cổng</span>
                </div>
            `;

            card.addEventListener('click', () => {
                loadGameMapFromCatalog(map.id);
                if (gameMapsModalEl) gameMapsModalEl.classList.add('hidden');
            });

            grid.appendChild(card);
        });
    }

    function filterGameMapsCatalog(query = '', category = 'all') {
        const allMaps = window.MAPS_CATALOG || [];
        const q = query.trim().toLowerCase();

        const filtered = allMaps.filter(m => {
            const matchesQuery = !q || m.id.toString() === q || m.name.toLowerCase().includes(q);
            if (!matchesQuery) return false;

            if (category === 'all') return true;
            if (category === 'village') {
                return m.name.toLowerCase().includes('làng') || m.name.toLowerCase().includes('thị trấn') || m.name.toLowerCase().includes('fosha') || m.name.toLowerCase().includes('syrup') || m.name.toLowerCase().includes('orange');
            }
            if (category === 'sea') {
                return m.name.toLowerCase().includes('biển') || m.name.toLowerCase().includes('bến tàu') || m.name.toLowerCase().includes('tàu');
            }
            if (category === 'island') {
                return m.name.toLowerCase().includes('đảo') || m.name.toLowerCase().includes('rừng') || m.name.toLowerCase().includes('núi') || m.name.toLowerCase().includes('vách');
            }
            if (category === 'event') {
                return m.name.toLowerCase().includes('namie') || m.name.toLowerCase().includes('kho báu') || m.name.toLowerCase().includes('phó bản') || m.id === 513;
            }
            if (category === 'pvp') {
                return m.name.toLowerCase().includes('đấu trường') || m.name.toLowerCase().includes('lôi đài') || m.name.toLowerCase().includes('chiến trường');
            }
            return true;
        });

        renderGameMapsCatalog(filtered);
    }

    function loadGameMapFromCatalog(mapId) {
        if (!window.MAPS_CATALOG || !window.MAPS_CATALOG.length) {
            showToast('Chưa tải được kho bản đồ MAPS_CATALOG!');
            return false;
        }
        const map = window.MAPS_CATALOG.find(m => m.id === mapId);
        if (!map) {
            showToast(`Không tìm thấy Bản đồ ID ${mapId} trong kho game!`);
            return false;
        }

        const ok = parseAndImportData(map.data, map.mapBack, map.id, map.name);
        if (ok) {
            // Parse NPCs
            state.npcs = [];
            try {
                const npcArr = JSON.parse(map.npcs || '[]');
                if (Array.isArray(npcArr)) {
                    state.npcs = npcArr.map(n => ({
                        iditem: n[0],
                        name: n[1] || 'NPC',
                        namegt: n[2] || '',
                        chat: n[3] || '',
                        x: n[4] || 0,
                        y: n[5] || 0,
                        typeIcon: n[7] || -1
                    }));
                }
            } catch (e) {
                console.error('Error parsing NPCs:', e);
            }

            // Parse Mobs
            state.mobs = [];
            try {
                const mobArr = JSON.parse(map.mobs || '[]');
                if (Array.isArray(mobArr)) {
                    state.mobs = mobArr.map((m, idx) => ({
                        mobId: m[0],
                        x: m[1] || 0,
                        y: m[2] || 0,
                        index: idx
                    }));
                }
            } catch (e) {
                console.error('Error parsing Mobs:', e);
            }

            // Parse Vgos
            state.vgos = [];
            try {
                const vgoArr = JSON.parse(map.vgos || '[]');
                if (Array.isArray(vgoArr)) {
                    state.vgos = vgoArr.map(v => ({
                        id_map_go: v[0],
                        xold: v[1] || 0,
                        yold: v[2] || 0,
                        xnew: v[3] || 0,
                        ynew: v[4] || 0
                    }));
                }
            } catch (e) {
                console.error('Error parsing Vgos:', e);
            }

            state.mapName = map.name;
            if (mapNameInput) mapNameInput.value = map.name;
            state.mapId = map.id;
            if (mapIdInput) mapIdInput.value = map.id;

            requestRedraw();
            updatePlacedObjectsList();
            showToast(`Đã nạp thành công Map #${map.id}: ${map.name}!`);
            return true;
        }
        return false;
    }

    // SQL EXPORTER LOGIC
    function generateSQL() {
        const mapId = parseInt(mapIdInput.value) || 513;

        // 1. Tile data[0]
        const data0 = [state.cols, state.rows, state.tileSetId || 5];
        for (let r = 0; r < state.rows; r++) {
            for (let c = 0; c < state.cols; c++) {
                data0.push(state.tiles[r] ? state.tiles[r][c] : 0);
            }
        }

        // 2. ItemMap binary data[1]
        const data1 = [];
        const numItems = state.placedObjects.length;
        data1.push((numItems >> 8) & 0xff);
        data1.push(numItems & 0xff);

        for (const obj of state.placedObjects) {
            const tid = obj.templateId || (500 + obj.iconId);
            const tx = obj.tileX;
            const ty = obj.tileY;

            data1.push((tid >> 8) & 0xff);
            data1.push(tid & 0xff);
            data1.push((tx >> 8) & 0xff);
            data1.push(tx & 0xff);
            data1.push((ty >> 8) & 0xff);
            data1.push(ty & 0xff);
        }

        const data0Signed = data0.map(b => b > 127 ? b - 256 : b);
        const data1Signed = data1.map(b => b > 127 ? b - 256 : b);

        const fullDataStr = JSON.stringify([data0Signed, data1Signed]);
        const sql = `UPDATE \`maps\` SET \`data\` = '${fullDataStr}', \`MapBack\` = '${state.mapBackStr}' WHERE \`id\` = ${mapId};\n`;
        return sql;
    }

    function openExportModal() {
        const sql = generateSQL();
        sqlOutputTextareaEl.value = sql;
        document.getElementById('statTilesCount').textContent = state.cols * state.rows;
        document.getElementById('statObjectsCount').textContent = state.placedObjects.length;
        document.getElementById('statSqlLength').textContent = (sql.length / 1024).toFixed(1) + ' KB';
        exportModalEl.classList.remove('hidden');
    }

    function copySqlToClipboard() {
        navigator.clipboard.writeText(sqlOutputTextareaEl.value).then(() => {
            document.getElementById('copySqlText').textContent = '✓ Đã Chép!';
            showToast('Đã sao chép lệnh SQL vào bộ nhớ tạm!');
            setTimeout(() => {
                document.getElementById('copySqlText').textContent = 'Sao Chép SQL';
            }, 2000);
        });
    }

    function downloadSqlFile() {
        const sql = generateSQL();
        const blob = new Blob([sql], { type: 'text/sql;charset=utf-8' });
        const a = document.createElement('a');
        a.href = URL.createObjectURL(blob);
        a.download = `update_map_${mapIdInput.value}.sql`;
        a.click();
        showToast('Đã tải xuống file update_map.sql');
    }

    function downloadProjectJson() {
        const data = {
            mapId: state.mapId,
            mapName: state.mapName,
            cols: state.cols,
            rows: state.rows,
            bgId: state.bgId,
            mapBackStr: state.mapBackStr,
            terrain: state.activeTerrain,
            placedObjects: state.placedObjects
        };
        const blob = new Blob([JSON.stringify(data, null, 2)], { type: 'application/json' });
        const a = document.createElement('a');
        a.href = URL.createObjectURL(blob);
        a.download = `map_project_${state.mapId}.json`;
        a.click();
        showToast('Đã lưu file dự án JSON!');
    }

    // IMPORT MAP DATA PARSER
    function parseAndImportData(rawText, directMapBack = null, directMapId = null, directMapName = null) {
        if (!rawText || !rawText.trim()) {
            showToast('Vui lòng dán dữ liệu hoặc câu lệnh SQL!');
            return false;
        }

        let text = rawText.trim();
        let mapId = directMapId;
        let mapBackStr = directMapBack;
        let dataJson = null;

        // 1. Check if it's a SQL UPDATE query
        const updateMatch = text.match(/UPDATE\s+[`"']?maps[`"']?\s+SET\s+(.*?)\s+WHERE\s+[`"']?id[`"']?\s*=\s*(\d+)/i);
        if (updateMatch) {
            mapId = parseInt(updateMatch[2]);
            const setClause = updateMatch[1];
            
            const dataMatch = setClause.match(/[`"']?data[`"']?\s*=\s*['"](.*?)['"](\s*,|\s*$)/i);
            if (dataMatch) {
                try {
                    dataJson = JSON.parse(dataMatch[1]);
                } catch (e) {
                    console.error('Failed to parse SQL data JSON:', e);
                }
            }

            const mapBackMatch = setClause.match(/[`"']?MapBack[`"']?\s*=\s*['"](.*?)['"](\s*,|\s*$)/i);
            if (mapBackMatch) {
                mapBackStr = mapBackMatch[1];
            }
        }

        // 2. Fallback: Parse raw JSON array
        if (!dataJson) {
            const jsonStart = text.indexOf('[');
            const jsonEnd = text.lastIndexOf(']');
            if (jsonStart !== -1 && jsonEnd !== -1 && jsonEnd > jsonStart) {
                const subStr = text.substring(jsonStart, jsonEnd + 1);
                try {
                    dataJson = JSON.parse(subStr);
                } catch (e) {
                    try {
                        dataJson = JSON.parse(subStr.replace(/'/g, '"'));
                    } catch (e2) {
                        console.error('Raw JSON parse error:', e2);
                    }
                }
            }
        }

        if (!dataJson || !Array.isArray(dataJson)) {
            showToast('Không tìm thấy mảng JSON [data] hợp lệ!');
            return false;
        }

        let data0 = [];
        let data1 = [];

        if (Array.isArray(dataJson[0])) {
            data0 = dataJson[0];
            if (dataJson.length > 1 && Array.isArray(dataJson[1])) {
                data1 = dataJson[1];
            }
        } else {
            data0 = dataJson;
        }

        // Convert signed bytes back to unsigned (0..255)
        const udata0 = data0.map(b => (b < 0 ? b + 256 : b));
        const udata1 = data1.map(b => (b < 0 ? b + 256 : b));

        if (udata0.length < 3) {
            showToast('Dữ liệu data[0] quá ngắn!');
            return false;
        }

        const cols = udata0[0];
        const rows = udata0[1];
        const tileType = udata0[2];

        if (cols <= 0 || rows <= 0 || cols > 200 || rows > 200) {
            showToast(`Kích thước map không hợp lệ: ${cols}x${rows}`);
            return false;
        }

        if (mapId) {
            state.mapId = mapId;
            if (mapIdInput) mapIdInput.value = mapId;
        }
        if (directMapName) {
            state.mapName = directMapName;
            if (mapNameInput) mapNameInput.value = directMapName;
        }

        state.cols = cols;
        state.rows = rows;
        canvas.width = cols * TILE_SIZE;
        canvas.height = rows * TILE_SIZE;

        const sizeKey = `${cols}x${rows}`;
        let matchedSize = false;
        for (let i = 0; i < mapSizeSelect.options.length; i++) {
            if (mapSizeSelect.options[i].value === sizeKey) {
                mapSizeSelect.selectedIndex = i;
                matchedSize = true;
                break;
            }
        }
        if (!matchedSize) {
            // Add custom size option
            const opt = document.createElement('option');
            opt.value = sizeKey;
            opt.textContent = `${cols} x ${rows} (${cols*24} x ${rows*24} px)`;
            mapSizeSelect.appendChild(opt);
            mapSizeSelect.value = sizeKey;
        }

        state.tiles = [];
        let tileIdx = 3;
        for (let r = 0; r < rows; r++) {
            const rowArr = [];
            for (let c = 0; c < cols; c++) {
                if (tileIdx < udata0.length) {
                    rowArr.push(udata0[tileIdx++]);
                } else {
                    rowArr.push(0);
                }
            }
            state.tiles.push(rowArr);
        }

        state.tileSetId = tileType || 5;
        if (state.tileSetId === 5) {
            state.activeTerrain = 'sand';
        } else if (state.tileSetId === 4 || state.tileSetId === 1) {
            state.activeTerrain = 'grass';
        } else {
            state.activeTerrain = 'stone';
        }

        if (mapBackStr) {
            state.mapBackStr = mapBackStr;
            try {
                const mbArr = JSON.parse(mapBackStr);
                if (Array.isArray(mbArr) && mbArr.length > 0) {
                    if (Array.isArray(mbArr[0])) {
                        // e.g. [[44,408,240], ...]
                        state.bgId = mbArr[0][0];
                    } else {
                        // e.g. [7, 270, 984, 408]
                        state.bgId = mbArr[0];
                    }
                    getBgImage(state.bgId);
                }
            } catch (e) {}
        }

        // Parse ItemMap binary data[1]
        state.placedObjects = [];
        if (udata1.length >= 2) {
            const numItems = (udata1[0] << 8) | udata1[1];
            let offset = 2;
            for (let i = 0; i < numItems; i++) {
                if (offset + 6 <= udata1.length) {
                    const tid = (udata1[offset] << 8) | udata1[offset + 1];
                    const tx = (udata1[offset + 2] << 8) | udata1[offset + 3];
                    const ty = (udata1[offset + 4] << 8) | udata1[offset + 5];
                    offset += 6;

                    const manifest = (window.TEMPLATE_MANIFEST && window.TEMPLATE_MANIFEST[tid]) || null;
                    let imgId = tid;
                    let dx = 0;
                    let dy = 0;
                    let layer = 3;

                    if (manifest) {
                        imgId = manifest.imgId;
                        dx = manifest.dx || 0;
                        dy = manifest.dy || 0;
                        layer = manifest.layer !== undefined ? manifest.layer : 3;
                    } else if (tid === 580) { imgId = 80; }
                    else if (tid === 581) { imgId = 81; }
                    else if (tid === 582) { imgId = 82; }
                    else if (tid === 583) { imgId = 83; }
                    else if (tid === 584) { imgId = 84; }
                    else if (tid > 500 && tid < 600) { imgId = tid - 500; }

                    getIconImage(imgId);

                    const obj = {
                        id: 'obj_' + Date.now() + '_' + i + '_' + Math.random().toString(36).substr(2, 4),
                        iconId: imgId,
                        imgId: imgId,
                        templateId: tid,
                        name: (manifest ? `Vật thể #${tid} (Img ${imgId})` : (tid === 581 ? 'Rương Kho Báu' : `Vật thể #${tid}`)),
                        x: tx * TILE_SIZE,
                        y: ty * TILE_SIZE,
                        tileX: tx,
                        tileY: ty,
                        dx: dx,
                        dy: dy,
                        layer: layer,
                        width: 48,
                        height: 48
                    };

                    if (!ITEM_CATALOG.some(c => c.iconId === imgId)) {
                        ITEM_CATALOG.push({
                            iconId: imgId,
                            templateId: tid,
                            name: obj.name,
                            layer: layer,
                            width: 48,
                            height: 48
                        });
                    }

                    state.placedObjects.push(obj);
                }
            }
        }

        renderPalette();
        updatePlacedObjectsList();
        selectObject(state.placedObjects[0] || null);
        applyZoom();
        requestRedraw();
        saveState();

        return true;
    }

    function loadPreset(presetKey) {
        if (presetKey === 'nami_orange') {
            if (window.MAPS_CATALOG) {
                loadGameMapFromCatalog(513);
            }
        }
    }

    function showToast(msg) {
        if (!toastEl) return;
        document.getElementById('toastMessage').textContent = msg;
        toastEl.classList.add('show');
        setTimeout(() => toastEl.classList.remove('show'), 3000);
    }

    function saveState() {
        state.history = state.history.slice(0, state.historyIndex + 1);
        state.history.push(JSON.stringify(state.placedObjects));
        state.historyIndex++;
    }

    function updateUI() {
        applyZoom();
        requestRedraw();
        updatePlacedObjectsList();
    }

})();
